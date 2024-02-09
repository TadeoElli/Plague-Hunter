using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPatrolState : IState
{
    RatController _rat;
    Vector3 newPosition;
    float timer;

    FSM<RatStates> _fsm;

    public RatPatrolState(FSM<RatStates> fsm, RatController rat)
    {
        _rat = rat;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        timer = 0f;
        newPosition = new Vector3(Random.Range(_rat.transform.position.x - 5f, _rat.transform.position.x + 5f), 0,Random.Range(_rat.transform.position.z - 5f, _rat.transform.position.z + 5f));
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
        _rat.transform.position = Vector3.MoveTowards(_rat.transform.position, newPosition, _rat.enemy.speed * Time.deltaTime);
        _rat.transform.LookAt(newPosition);
        if(Vector3.Distance(_rat.transform.position, newPosition) < _rat.enemy.attackDistance)
        {
            _fsm.ChangeState(RatStates.Idle);
        }
        if(timer > _rat.enemy.timeToPatrol)
        {
            _fsm.ChangeState(RatStates.Idle);
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
