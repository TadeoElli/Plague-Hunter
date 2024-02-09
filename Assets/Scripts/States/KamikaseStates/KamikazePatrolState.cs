using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazePatrolState : IState
{
    KamikazeEnemy _kamikaze;
    Vector3 newPosition;
    float timer;

    FSM<KamikazeStates> _fsm;

    public KamikazePatrolState(FSM<KamikazeStates> fsm, KamikazeEnemy kamikaze)
    {
        _kamikaze = kamikaze;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        timer = 0f;
        newPosition = new Vector3(Random.Range(_kamikaze.transform.position.x - 5f, _kamikaze.transform.position.x + 5f), 0,Random.Range(_kamikaze.transform.position.z - 5f, _kamikaze.transform.position.z + 5f));
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
        _kamikaze.transform.position = Vector3.MoveTowards(_kamikaze.transform.position, newPosition, _kamikaze.enemy.speed * Time.deltaTime);
        _kamikaze.transform.LookAt(newPosition);
        if(Vector3.Distance(_kamikaze.transform.position, newPosition) < 1f)
        {
            _fsm.ChangeState(KamikazeStates.Idle);
        }
        if(timer > _kamikaze.enemy.timeToPatrol)
        {
            _fsm.ChangeState(KamikazeStates.Idle);
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
