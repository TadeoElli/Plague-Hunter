using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatIdleState : IState
{
    RatController _rat;
    float timer, recoveryTime;


    FSM<RatStates> _fsm;

    public RatIdleState(FSM<RatStates> fsm, RatController rat)
    {
        _rat = rat;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        recoveryTime = 10f;
        timer = 0f;
        
    }

    public void OnUpdate()
    {
        if(_rat.IFOV() || Vector3.Distance(_rat.transform.position, _rat.target.position) < _rat.enemy.attackDistance)
        {
            if(!_rat.player.isStealth)
            {
                _fsm.ChangeState(RatStates.Following);
            }
        }
    }

    public void OnFixedUpdate()
    {
        if(Vector3.Distance(_rat.transform.position, _rat.originPosition) > 10f)
        {
            _fsm.ChangeState(RatStates.Return);
        }
        else
        {
            if(timer > recoveryTime)
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
