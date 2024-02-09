using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TPFinal - Tadeo Elli.
public class KamikazeEnemy : EnemyBase
{
    public GameObject explosion;    

    public bool willExplode = false;
    [SerializeField] public  Animator animator;
    [SerializeField] AnimEvents events;

    FSM<KamikazeStates> _FSM;

    [HideInInspector]public Vector3 originPosition;
    [SerializeField] public LayerMask obstacleLayer;
    public EnemyFov enemy = new EnemyFov(4, 2, 10, 8.68f, 60.77f);

    void Start()
    {
        base.Start();
        originPosition = this.transform.position;
        _FSM = new FSM<KamikazeStates>();      //Creo una nueva lista de estados
        events.ADD_EVENT("Explotion", startRealExplotion);  //Agrego los eventos de la animacion de ataque

        IState idle = new KamikazeIdleState(_FSM, this);        //Agrego los estados
        _FSM.AddState(KamikazeStates.Idle, new KamikazeIdleState(_FSM, this));
        _FSM.AddState(KamikazeStates.Patrol, new KamikazePatrolState(_FSM, this));
        _FSM.AddState(KamikazeStates.Return, new KamikazeReturnState(_FSM, this));
        _FSM.AddState(KamikazeStates.Following, new KamikazeFollowState(_FSM, this));
        _FSM.AddState(KamikazeStates.Attacking, new KamikazeAttackState(_FSM, this));

        _FSM.ChangeState(KamikazeStates.Idle);     //Cambio al estado idle
    }


    void Update()
    {
        _FSM.Update();
        _FSM.FixedUpdate();

    }
    
    
    void startRealExplotion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        GameManager.GetAudioManager().PlayClipByName("KamikazeExplosion");
        Destroy(gameObject);
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
