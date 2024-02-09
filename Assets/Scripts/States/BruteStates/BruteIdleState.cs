using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteIdleState : IState
{
    Brute _brute;
    float timer;


    FSM<BruteStates> _fsm;

    public BruteIdleState(FSM<BruteStates> fsm, Brute brute)
    {
        _brute = brute;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        timer = 0f;
        _brute.animator.SetBool("IsMoving", false);
        
    }

    public void OnUpdate()
    {
        if(_brute.IFOV() || Vector3.Distance(_brute.transform.position, _brute.target.position) < _brute.enemy.attackDistance * 2f )
        {
            if(!_brute.player.isStealth)
            {
                _fsm.ChangeState(BruteStates.Following);
            }
        }
    }

    public void OnFixedUpdate()
    {
        if(Vector3.Distance(_brute.transform.position, _brute.originPosition) > 10f)
        {
            _fsm.ChangeState(BruteStates.Return);
        }
        else
        {
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
