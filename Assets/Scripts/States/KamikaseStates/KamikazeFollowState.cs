using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeFollowState : IState
{
    KamikazeEnemy _kamikaze;

    FSM<KamikazeStates> _fsm;

    public KamikazeFollowState(FSM<KamikazeStates> fsm, KamikazeEnemy kamikaze)
    {
        _kamikaze = kamikaze;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        if(Vector3.Distance(_kamikaze.transform.position, _kamikaze.target.position) > _kamikaze.enemy._viewRadius)
        {
            _fsm.ChangeState(KamikazeStates.Patrol);
        }
        Debug.Log("Entre al follow");
    }

    public void OnUpdate()
    {
        if(Vector3.Distance(_kamikaze.transform.position, _kamikaze.target.position) < _kamikaze.enemy.attackDistance && !_kamikaze.willExplode)
        {
            _fsm.ChangeState(KamikazeStates.Attacking);
        }
        else if(Vector3.Distance(_kamikaze.transform.position, _kamikaze.target.position) > _kamikaze.enemy._viewRadius * 1.5f)
        {
            _fsm.ChangeState(KamikazeStates.Idle);
        }
        else if(Vector3.Distance(_kamikaze.transform.position, _kamikaze.player.lastPosition) < _kamikaze.enemy.attackDistance && _kamikaze.player.isStealth)
        {
            _fsm.ChangeState(KamikazeStates.Attacking);
        }
        else
        {
            if(!_kamikaze.player.isStealth)
            {
                _kamikaze.transform.position = Vector3.MoveTowards(_kamikaze.transform.position,_kamikaze.target.position, _kamikaze.enemy.speed * Time.deltaTime);
                _kamikaze.transform.LookAt(_kamikaze.target.position);
            }
            else
            {
                _kamikaze.transform.position = Vector3.MoveTowards(_kamikaze.transform.position,_kamikaze.player.lastPosition, _kamikaze.enemy.speed * Time.deltaTime);
                _kamikaze.transform.LookAt(_kamikaze.player.lastPosition);
            }
        }
    }

    public void OnFixedUpdate()
    {

    }

    public void OnExit()
    {
        Debug.Log("Sali del follow");
    }

}
