using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeAttackState : IState
{
    KamikazeEnemy _kamikaze;

    FSM<KamikazeStates> _fsm;

    public KamikazeAttackState(FSM<KamikazeStates> fsm, KamikazeEnemy kamikaze)
    {
        _kamikaze = kamikaze;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        _kamikaze.animator.SetTrigger("WillExplode");
        _kamikaze.enemy.speed = _kamikaze.enemy.speed / 2;
        _kamikaze.willExplode = true;
        _fsm.ChangeState(KamikazeStates.Following);
    }

    public void OnUpdate()
    {

    }

    public void OnFixedUpdate()
    {

    }

    public void OnExit()
    {
        
    }

}
