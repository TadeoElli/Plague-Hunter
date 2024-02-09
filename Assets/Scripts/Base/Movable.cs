using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    Rigidbody myRig;

    [SerializeField] float baseSpeed;
    [SerializeField] float extraRunSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float speedMultiplier = 1;

    private enum State
    {
        Dash,
        Walk,
        Run,
        Stop,
    }

    private bool movementEnabled = true;
    private State state = State.Stop;
    //Vector3 direction { get => _direction; set => _direction = value.normalized; }
    Vector3 _direction;
    Vector3 direction { get => _direction; set => _direction = value.magnitude > 1 ? value.normalized : value; }


    // Start is called before the first frame update
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
        movementEnabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case State.Walk:
                WalkState();
                break;
            case State.Run:
                RunState();
                break;
            case State.Dash:
                DashState();
                break;
            case State.Stop:
                StopState();
                break;
            default:
                state = State.Stop;
                break;
        }
    }
    void StopState()
    {
        transform.Rotate(0, 0, 0);
        transform.Translate(0, 0, 0);
        myRig.velocity = Vector3.zero;
    }
    void WalkState()
    {
        myRig.velocity = direction * baseSpeed * speedMultiplier * Time.fixedDeltaTime;
    }
    void RunState()
    {
        myRig.velocity = direction * (baseSpeed + extraRunSpeed) * speedMultiplier * Time.fixedDeltaTime;
    }
    void DashState()
    {
        myRig.velocity = direction * (baseSpeed + dashSpeed) * Time.fixedDeltaTime;
    }

    public void Walk(Vector3 dir)
    {
        if (movementEnabled)
        {
            direction = dir;
            state = State.Walk;
        }
    }
    public void Run(Vector3 dir)
    {
        if (movementEnabled)
        {
            direction = dir;
            state = State.Run;
        }
    }

    public void Dash(Vector3 dir)
    {
        if (movementEnabled)
        {
            direction = dir;
            state = State.Dash;
        }
    }

    public void StopMoving()
    {
        state = State.Stop;
    }
    public void BlockMovement()
    {
        movementEnabled = false;
        state = State.Stop;
    }
    public void EnableMovement()
    {
        movementEnabled = true;
    }

    public bool isMovementEnabled() => movementEnabled;
}
