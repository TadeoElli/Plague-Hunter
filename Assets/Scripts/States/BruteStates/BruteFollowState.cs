using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteFollowState : IState
{
    Brute _brute;
    FSM<BruteStates> _fsm;

    public BruteFollowState(FSM<BruteStates> fsm, Brute brute)
    {
        _brute = brute;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        _brute.animator.SetBool("IsMoving", true);
        if(Vector3.Distance(_brute.transform.position, _brute.target.position) > _brute.enemy._viewRadius)
        {
            _fsm.ChangeState(BruteStates.Patrol);
        }
    }

    public void OnUpdate()
    {
        if(Vector3.Distance(_brute.transform.position, _brute.target.position) < _brute.enemy.attackDistance)
        {
            
            _fsm.ChangeState(BruteStates.Attacking);
        }
        else if(Vector3.Distance(_brute.transform.position, _brute.target.position) > _brute.enemy._viewRadius * 1.5f)
        {
            _fsm.ChangeState(BruteStates.Idle);
        }
        else if(Vector3.Distance(_brute.transform.position, _brute.player.lastPosition) < _brute.enemy.attackDistance && _brute.player.isStealth)
        {
            _fsm.ChangeState(BruteStates.Idle);
        }
        else
        {
            if(!_brute.player.isStealth)
            {
                _brute.transform.position = Vector3.MoveTowards(_brute.transform.position,_brute.target.position, _brute.enemy.speed * Time.deltaTime);
                _brute.transform.LookAt(_brute.target.position);
            }
            else
            {
                _brute.transform.position = Vector3.MoveTowards(_brute.transform.position,_brute.player.lastPosition, _brute.enemy.speed * Time.deltaTime);
                _brute.transform.LookAt(_brute.player.lastPosition);
            }
        }
    }

    public void OnFixedUpdate()
    {
        
    }

    public void OnExit()
    {
        _brute.canAttack = false;
    }

}
