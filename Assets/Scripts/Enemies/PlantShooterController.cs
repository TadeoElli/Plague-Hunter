using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TPFinal - Tadeo Elli.

public class PlantShooterController : EnemyBase
{
    // Start is called before the first frame update
    [Range(0.5f, 2.5f)] public float AttackCooldown = 1.5f;
    [Range(3f, 10f)] public float changeDirSpeed = 5f;
    [Range(5f, 20f)] public float FollowingDistance = 10f;
    [Range(6f, 15f)] public float attackDistance;
    private bool isAttackOnCooldown = false;
    private State state = State.Idle;

    [SerializeField] Bullet model;


    private Renderer myRenderer;
    private Material originalMaterial;

    

    private enum State
    {
        Idle,
        Following,
        Attacking,
        Death
    }

    protected override void Start()
    {
        base.Start();
        dmgReceiver.setDeathCallback(DeathCallback);
        dmgReceiver.setOnTakeDamageCallback(ReceiveDamage);
        myRenderer = GetComponentInChildren<Renderer>();
        originalMaterial = myRenderer.material;

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            switch (state)
            {
                case State.Idle:
                    //Follow
                    if (OnFollowDistance() && !player.isStealth)
                    {
                        state = State.Following;
                        break;
                    }
                    //Attack
                    else if (OnAttackDistance() && !isAttackOnCooldown && !player.isStealth)
                    {
                        state = State.Attacking;
                        break;
                    }
                    Idle();
                    break;

                case State.Following:
                    //Idle (Unfollow)
                    if (!OnFollowDistance() || player.isStealth)
                    {
                        state = State.Idle;
                        break;
                    }
                    //Attack
                    else if (OnAttackDistance() && !isAttackOnCooldown && !player.isStealth)
                    {
                        state = State.Attacking;
                        break;
                    }
                    Following();
                    break;

                case State.Attacking:
                    Attacking();
                    state = State.Following;
                    break;

                case State.Death:
                    Death();
                    break;

                default:
                    state = State.Idle;
                    break;
            }
        }
    }
    
    void ReceiveDamage(Damageable dmg)
    {
        myRenderer.material = Resources.Load<Material>("Art/Materials/red");
        Invoke("FinishReceiveDamage", 0.3f);
    }
    void FinishReceiveDamage()
    {
        myRenderer.material = originalMaterial;
    }
    void DeathCallback(Damageable dmg)
    {
        state = State.Death;
    }
    private bool OnFollowDistance()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        //Debug.Log(distance);
        if (distance <= FollowingDistance)
            return true;
        return false;
    }
    private bool OnAttackDistance()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < attackDistance)
            return true;
        return false;
    }
    void StartAttack()
    {
        
    }

    void EnableAttack() => isAttackOnCooldown = false;
    void DisableAttack() => isAttackOnCooldown = true;

    void Idle()
    {

    }
    void Following()
    {
        LookAt();
    }
    void LookAt()
    {
        Vector3 flechita = target.position - transform.position;
        flechita.y = 0;
        transform.forward = Vector3.Slerp(transform.forward, flechita, Time.deltaTime * changeDirSpeed);
    }
    void Attacking()
    {
        GameManager.GetAudioManager().PlayClipByName("PlantShooting");

        Bullet bullet = GameObject.Instantiate(model);
        bullet.Move(transform.position, transform.forward);
        bullet.parent = this.gameObject;
        DisableAttack();
        Invoke("EnableAttack", AttackCooldown);
    }
    void Death()
    {
        Debug.Log("planta muriendo");
        Destroy(this.gameObject);
    }
}
