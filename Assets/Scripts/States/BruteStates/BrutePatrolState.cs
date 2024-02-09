using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrutePatrolState : IState
{
    Brute _brute;
    Vector3 newPosition;
    float timer;


    FSM<BruteStates> _fsm;

    public BrutePatrolState(FSM<BruteStates> fsm, Brute brute)
    {
        _brute = brute;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        timer = 0f;
        _brute.animator.SetBool("IsMoving", true);
        newPosition = new Vector3(Random.Range(_brute.transform.position.x - 5f, _brute.transform.position.x + 5f), 0,Random.Range(_brute.transform.position.z - 5f, _brute.transform.position.z + 5f));
    }

    public void OnUpdate()
    {
        if(_brute.IFOV() || Vector3.Distance(_brute.transform.position, _brute.target.position) < _brute.enemy.attackDistance * 2f)
        {
            if(!_brute.player.isStealth)
            {
                _fsm.ChangeState(BruteStates.Following);
            }
        }
    }

    public void OnFixedUpdate()
    {
        _brute.transform.position = Vector3.MoveTowards(_brute.transform.position, newPosition, _brute.enemy.speed * Time.deltaTime);
        _brute.transform.LookAt(newPosition);
        if(Vector3.Distance(_brute.transform.position, newPosition) < _brute.enemy.attackDistance)
        {
            _fsm.ChangeState(BruteStates.Idle);
        }
        if(timer > _brute.enemy.timeToPatrol)
        {
            _fsm.ChangeState(BruteStates.Idle);
        }
        else
        {
            timer = timer + 1f * Time.deltaTime;
        }
    }

    public void OnExit()
    {
        
    }

}
