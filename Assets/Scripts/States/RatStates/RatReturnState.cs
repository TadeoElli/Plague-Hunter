using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatReturnState : IState
{
    RatController _rat;
    float timer;

    FSM<RatStates> _fsm;

    public RatReturnState(FSM<RatStates> fsm, RatController rat)
    {
        _rat = rat;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        timer = 0f;
    }

    public void OnUpdate()
    {
        if(_rat.IFOV() || Vector3.Distance(_rat.transform.position, _rat.target.position) < _rat.enemy.attackDistance * 2f)
        {
            if(!_rat.player.isStealth)
            {
                _fsm.ChangeState(RatStates.Following);
            }
        }
    }

    public void OnFixedUpdate()
    {
        if(Vector3.Distance(_rat.transform.position, _rat.originPosition) < _rat.enemy.attackDistance)
        {
            _fsm.ChangeState(RatStates.Idle);
        }
        else
        {
            _rat.transform.position = Vector3.MoveTowards(_rat.transform.position, _rat.originPosition, _rat.enemy.speed * Time.deltaTime);
            _rat.transform.LookAt(_rat.originPosition);

            if(timer > _rat.enemy.timeToPatrol)
            {
                _fsm.ChangeState(RatStates.Patrol);
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
