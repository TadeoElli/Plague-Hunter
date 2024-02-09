using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
//TPFinal - Tadeo Elli.
public class BossEnemy : EnemyBase
{
    // Start is called before the first frame update
    [SerializeField] public  Animator animator;
    [SerializeField] private AnimEvents events;
    [SerializeField] public GameObject throwPoint, previewRock, dustExplosion, iceCage;
    //[SerializeField] public ParticleSystem DustExplosion;
    FSM<BossStates> _FSM;
    [SerializeField]public MeleeSensor chargeMeleeSensor, jumpMeleeSensor;
    public Throwable throwRock;
    public float speed, chargeDistance, chargeCooldown, jumpCooldown, damage, maxthrowDistance, jumpDistance;
    public int charges= 0, jumps = 0;
    public bool hasChargeAttack, hasJumpAttack;
    float chargeTimer, jumpTimer;

    void Awake() 
    {
        base.Awake();
        //meleeSensor = this.GetComponentInChildren<MeleeSensor>();
    }
    void Start()
    {
        base.Start();
        _FSM = new FSM<BossStates>();      //Creo una nueva lista de estados
        chargeMeleeSensor.setDealDamageCallback(DealDamage);
        jumpMeleeSensor.setDealDamageCallback(DealDamage);
        dmgReceiver.setDeathCallback(DeathCallback);
        events.ADD_EVENT("instantiate_rock", CreateRock);  //Agrego los eventos de la animacion de ataque
        events.ADD_EVENT("start_Throw", StartThrowAttack);
        events.ADD_EVENT("jump_start", StartJump);
        events.ADD_EVENT("jump_attack_start", StartJumpAttack);
        events.ADD_EVENT("jump_attack_end", EndJumpAttack);
        events.ADD_EVENT("jump_end", EndJump);
        events.ADD_EVENT("death", Death);


        IState idle = new BossIdleState(_FSM, this);        //Agrego los estados
        _FSM.AddState(BossStates.Idle, new BossIdleState(_FSM, this));
        _FSM.AddState(BossStates.Throwing, new BossThrowState(_FSM, this));
        _FSM.AddState(BossStates.Charging, new BossChargeState(_FSM, this));
        _FSM.AddState(BossStates.IdleCharge, new BossIdleChargeState(_FSM, this));
        _FSM.AddState(BossStates.Jumping, new BossJumpState(_FSM, this));
        _FSM.AddState(BossStates.IdleJump, new BossIdleJumpState(_FSM, this));
        _FSM.AddState(BossStates.Frozen, new BossFrozenState(_FSM, this));
        _FSM.AddState(BossStates.Death, new BossDeathState(_FSM, this));

        _FSM.ChangeState(BossStates.Idle);     //Cambio al estado idle

        hasChargeAttack = true;
        hasJumpAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasChargeAttack)
        {
            if(chargeTimer > chargeCooldown)
            {
                hasChargeAttack = true;
            }
            else
            {
                chargeTimer = chargeTimer + 1 * Time.deltaTime;
            }
        }
        if(!hasJumpAttack)
        {
            if(jumpTimer > jumpCooldown)
            {
                hasJumpAttack = true;
            }
            else
            {
                jumpTimer = jumpTimer + 1 * Time.deltaTime;
            }
        }
        
        _FSM.Update();      //Llamo al update del estado actual
        _FSM.FixedUpdate();
    }

    void DealDamage(Damageable dmg)     //Funcion para hacer daño al objetivo que golpea y el tipo de daño
    {
        damage = (int)Random.Range(16f,22f);

        dmg.TakeDamage(damage, DamageTypes.basic);
    }
    public void SetChargeTimer()
    {
        chargeTimer = 0;
    }
    public void SetJumpTimer()
    {
        jumpTimer = 0;
    }
    void CreateRock()
    {
        previewRock.SetActive(true);
    }
    void StartThrowAttack()
    {
        previewRock.SetActive(false);
        var p = GameObject.Instantiate(throwRock);
        p.Throw(throwPoint.transform.position, target.position);
    }
    void StartJump()
    {
        jumps++;
        _FSM.ChangeState(BossStates.Jumping);
    }
    void StartJumpAttack()
    {
        jumpMeleeSensor.TurnOn();
        GameManager.GetAudioManager().PlayClipByName("BossFall");
        dustExplosion.SetActive(true);
    }
    void EndJumpAttack()
    {
        jumpMeleeSensor.TurnOff();
        dustExplosion.SetActive(false);
    }
    void EndJump()
    {
        animator.SetBool("IsJumping", false);
    }
    void DeathCallback(Damageable dmg)
    {
        Die();
    }

    void Die()      //Muere el enemigo
    {
        animator.SetTrigger("IsDeath");
        _FSM.ChangeState(BossStates.Death);
        GameManager.GetAudioManager().PlayClipByName("BossDeath");

        //TODO: aplicar animacion de muerte en el bicho
    }

    void Death()
    {
        GameManager.GoToNextLevel();
    }


    private void OnCollisionEnter(Collision other) 
    {
        int objLayer = other.gameObject.layer;
        if(objLayer == 9)
        {
            if(other.gameObject.tag == "IceTrap")
            {
                _FSM.ChangeState(BossStates.Frozen);
            }
            else
            {
                if(charges >= 3)
                {
                    _FSM.ChangeState(BossStates.Idle);
                }
                else
                {
                    _FSM.ChangeState(BossStates.IdleCharge);
                }
            }
        }

    }

}
