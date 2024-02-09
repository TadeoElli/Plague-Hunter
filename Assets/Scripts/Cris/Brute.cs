using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
//TPFinal - Tadeo Elli.
public class Brute : EnemyBase
{
    [SerializeField] public  Animator animator;
    [SerializeField] AnimEvents events;
    FSM<BruteStates> _FSM;
    //Rigidbody myRigidBody;
    [HideInInspector] public Vector3 originPosition;
    public float attackCooldown, damage;

    public bool canAttack = true;


    [SerializeField] public LayerMask obstacleLayer;
    public EnemyFov enemy = new EnemyFov(2.5f, 1, 10, 10, 90);
    //enemy.speed = speed;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        originPosition = this.transform.position;       //Guardo la posicion de spawn
        _FSM = new FSM<BruteStates>();      //Creo una nueva lista de estados
        meleeSensor.setDealDamageCallback(DealDamage);
        dmgReceiver.setDeathCallback(DeathCallback);

        events.ADD_EVENT("start_attack", StartRealAttack);  //Agrego los eventos de la animacion de ataque
        events.ADD_EVENT("end_attack", EndRealAttack);

        IState idle = new BruteIdleState(_FSM, this);        //Agrego los estados
        _FSM.AddState(BruteStates.Idle, new BruteIdleState(_FSM, this));
        _FSM.AddState(BruteStates.Patrol, new BrutePatrolState(_FSM, this));
        _FSM.AddState(BruteStates.Return, new BruteReturnState(_FSM, this));
        _FSM.AddState(BruteStates.Following, new BruteFollowState(_FSM, this));
        _FSM.AddState(BruteStates.Attacking, new BruteAttackState(_FSM, this));

        _FSM.ChangeState(BruteStates.Idle);     //Cambio al estado idle
    }

    void Update()
    {
        _FSM.Update();      //Llamo al update del estado actual
        _FSM.FixedUpdate();
        
    }

    void DealDamage(Damageable dmg)     //Funcion para hacer daño al objetivo que golpea y el tipo de daño
    {
        damage = (int)Random.Range(18f,23f);

        dmg.TakeDamage(damage, DamageTypes.basic);
    }

    void StartRealAttack()      //Activo el melee sensor para detectar si golpeo a alguien
    {
        meleeSensor.TurnOn();
        Debug.Log("brute real attack");
    }
    void EndRealAttack()        //Desactivo el melee sensor
    {
        meleeSensor.TurnOff();
    }

    void SetBoolCanAttack()     //Llamo a la funcion despues de tantos segundos para activar el ataque devuelta
    {
        canAttack = true;
    }

    void Die()      //Muere el enemigo
    {
        enemy.speed = 0f;
        animator.SetTrigger("Death");
        Destroy(gameObject, 2.5f);
        GameManager.GetAudioManager().PlayClipByName("BruteDeath");

        //TODO: aplicar animacion de muerte en el bicho
    }

    void DeathCallback(Damageable dmg)
    {
        Die();
    }

    private void OnDrawGizmos()     //Creo los guizmos del fov
    {
        var realAngle = enemy._viewAngle / 2;

        Gizmos.color = Color.magenta;
        Vector3 lineLeft = base.GetDirFromAngle(-realAngle + transform.eulerAngles.y);
        Gizmos.DrawLine(transform.position, transform.position + lineLeft * enemy._viewRadius);

        Vector3 lineRight = base.GetDirFromAngle(realAngle + transform.eulerAngles.y);
        Gizmos.DrawLine(transform.position, transform.position + lineRight * enemy._viewRadius);
    }


    public bool IFOV()
    {
        return base.InFieldOfView(target.position, enemy._viewRadius, enemy._viewAngle, obstacleLayer);
    }

}
