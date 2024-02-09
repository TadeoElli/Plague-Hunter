using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteReturnState : IState
{
    Brute _brute;
    float timer;
    FSM<BruteStates> _fsm;

    public BruteReturnState(FSM<BruteStates> fsm, Brute brute)
    {
        _brute = brute;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        _brute.animator.SetBool("IsMoving", true);
        timer = 0f;
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
        if(Vector3.Distance(_brute.transform.position, _brute.originPosition) < 2f)
        {
            _fsm.ChangeState(BruteStates.Idle);
        }
        else
        {
            _brute.transform.position = Vector3.MoveTowards(_brute.transform.position, _brute.originPosition, _brute.enemy.speed * Time.deltaTime);
            _brute.transform.LookAt(_brute.originPosition);

            if(timer > _brute.enemy.timeToPatrol)
            {
                _fsm.ChangeState(BruteStates.Patrol);
            }
            else
            {
                timer = timer + 1f * Time.deltaTime;
            }
        }
    }

    public void OnExit()
    {
        
    }

}
