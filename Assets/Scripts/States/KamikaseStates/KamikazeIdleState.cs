using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeIdleState : IState
{
    KamikazeEnemy _kamikaze;
    float timer;


    FSM<KamikazeStates> _fsm;

    public KamikazeIdleState(FSM<KamikazeStates> fsm, KamikazeEnemy kamikaze)
    {
        _kamikaze = kamikaze;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        timer = 0f;
        
    }

    public void OnUpdate()
    {
        if(_kamikaze.IFOV())
        {
            if(!_kamikaze.player.isStealth)
            {
                _fsm.ChangeState(KamikazeStates.Following);
            }
        }
    }

    public void OnFixedUpdate()
    {
        if(Vector3.Distance(_kamikaze.transform.position, _kamikaze.originPosition) > 10f)
        {
            _fsm.ChangeState(KamikazeStates.Return);
        }
        else
        {
            if(timer > _kamikaze.enemy.timeToPatrol)
            {
                
                _fsm.ChangeState(KamikazeStates.Patrol);
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
