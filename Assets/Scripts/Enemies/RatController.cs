using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

//TPFinal - Tadeo Elli.
public class RatController : EnemyBase
{

    [Range(0.5f, 2.5f)] public float attackCooldown = 1.5f;
    [Range(2f, 8f)] public float attackSpeed = 4f;
    [Range(3f, 10f)] public float changeDirSpeed = 5f;
    [HideInInspector] public Vector3 originPosition;

    FSM<RatStates> _FSM;
    public bool isAttackOnCooldown = false;

    public EnemyFov enemy = new EnemyFov(3, 3, 10, 12, 80);
    float basicAttackDamage = 2f;

    [SerializeField] public LayerMask obstacleLayer;



    protected override void Awake()
    {
        base.Awake();
        
        
        //Debug.Log("awake on rat");
    }

    protected override void Start()
    {
        base.Start();
        originPosition = this.transform.position;
        _FSM = new FSM<RatStates>();      //Creo una nueva lista de estados
        dmgReceiver.setDeathCallback(DeathCallback);
        meleeSensor.setDealDamageCallback(DealDamage);
        IState idle = new RatIdleState(_FSM, this);        //Agrego los estados
        _FSM.AddState(RatStates.Idle, new RatIdleState(_FSM, this));
        _FSM.AddState(RatStates.Patrol, new RatPatrolState(_FSM, this));
        _FSM.AddState(RatStates.Return, new RatReturnState(_FSM, this));
        _FSM.AddState(RatStates.Following, new RatFollowState(_FSM, this));
        _FSM.AddState(RatStates.Attacking, new RatAttackState(_FSM, this));

        _FSM.ChangeState(RatStates.Idle);     //Cambio al estado idle
    }

    private void Update()
    {
        _FSM.Update();
        _FSM.FixedUpdate();
    }

    void DealDamage(Damageable dmg)
    {
        if (dmg == dmgTarget)
        {
            basicAttackDamage = (int)Random.Range(2f,6f);
            dmg.TakeDamage(basicAttackDamage, DamageTypes.basic);
        }

        Vector3 dir = (-transform.forward * 2.2f) * attackSpeed;
        rig.AddForce(dir, ForceMode.Impulse);
        //rig.AddForceAtPosition(dir, ForceMode.Impulse);
    }
    void DeathCallback(Damageable dmg)
    {
        Death();
    }

    public void StartAttack()
    {
        meleeSensor.Attack(0.2f);
        DisableAttack();
        Invoke("EnableAttack", attackCooldown);
    }
    void EnableAttack() => isAttackOnCooldown = false;
    void DisableAttack() => isAttackOnCooldown = true;

    public void Attacking()
    {
        Vector3 dir = (transform.forward * 2 + transform.up) * attackSpeed;
        rig.AddForce(dir, ForceMode.Impulse);
    }

    void Death()
    {
        GameManager.GetAudioManager().PlayClipByName("ratDeath");
        Debug.Log("estoy muriendo");
        Destroy(this.gameObject);
    }
    
    void LookAt()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * changeDirSpeed);
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

