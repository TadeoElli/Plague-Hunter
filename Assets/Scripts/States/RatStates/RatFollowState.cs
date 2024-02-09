using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatFollowState : IState
{
    RatController _rat;

    FSM<RatStates> _fsm;

    public RatFollowState(FSM<RatStates> fsm, RatController rat)
    {
        _rat = rat;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        if(Vector3.Distance(_rat.transform.position, _rat.target.position) > _rat.enemy._viewRadius)
        {
            _fsm.ChangeState(RatStates.Patrol);
        }
    }

    public void OnUpdate()
    {
        if(Vector3.Distance(_rat.transform.position, _rat.target.position) < _rat.enemy.attackDistance && !_rat.isAttackOnCooldown)
        {
            _fsm.ChangeState(RatStates.Attacking);
        }
        else if(Vector3.Distance(_rat.transform.position, _rat.target.position) > _rat.enemy._viewRadius * 1.5f)
        {
            _fsm.ChangeState(RatStates.Idle);
        }
        else if(Vector3.Distance(_rat.transform.position, _rat.player.lastPosition) < _rat.enemy.attackDistance && _rat.player.isStealth)
        {
            _fsm.ChangeState(RatStates.Idle);
        }
        else
        {
            if(!_rat.player.isStealth)
            {
                _rat.transform.position = Vector3.MoveTowards(_rat.transform.position,_rat.target.position, _rat.enemy.speed * Time.deltaTime);
                _rat.transform.LookAt(_rat.target.position);
            }
            else
            {
                _rat.transform.position = Vector3.MoveTowards(_rat.transform.position,_rat.player.lastPosition, _rat.enemy.speed * Time.deltaTime);
                _rat.transform.LookAt(_rat.player.lastPosition);
            }
        }
    }

    public void OnFixedUpdate()
    {
        
    }


    public void OnExit()
    {
        
    }

}
