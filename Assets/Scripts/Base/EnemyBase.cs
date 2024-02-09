using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public Transform target;
    [HideInInspector] public Rigidbody rig;
    [HideInInspector] public Damageable dmgReceiver;
    [HideInInspector] public Damageable dmgTarget;
    [HideInInspector] public MeleeSensor meleeSensor;
    [HideInInspector] public PlayerController player;

    protected virtual void Awake()
    {
        rig = this.GetComponent<Rigidbody>();
        meleeSensor = this.GetComponentInChildren<MeleeSensor>();
    }
    protected virtual void Start()
    {
        dmgReceiver = this.GetComponent<Damageable>();
        player = GameManager.GetPlayer();
        target = player.transform;
        dmgTarget = player.GetComponent<Damageable>();
    }  

    protected virtual Vector3 GetDirFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public bool InFieldOfView(Vector3 targetPos, float _viewRadius, float _viewAngle, LayerMask obstacleLayer)        //Si esta en el angulo de vision
    {
        Vector3 dir = targetPos - transform.position;

        //Que este dentro de la distancia maxima de vision
        if (dir.sqrMagnitude > _viewRadius * _viewRadius) return false;

        //Que no haya obstaculos
        if (InLineOfSight(dir, _viewRadius, obstacleLayer)) return false;

        //Que este dentro del angulo
        return Vector3.Angle(transform.forward, dir) <= _viewAngle/2;
        

    }

    public bool InLineOfSight(Vector3 direction, float _viewRadius, LayerMask obstacleLayer)        //Si esta en linea de vision
    {
        Debug.DrawLine(transform.position, direction, Color.red);
        return Physics.Raycast(transform.position, direction, _viewRadius, obstacleLayer);
    } 
}
