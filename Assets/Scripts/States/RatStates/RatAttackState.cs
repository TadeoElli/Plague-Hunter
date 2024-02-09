using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAttackState : IState
{
    RatController _rat;
    float timer, recoveryTime;


    FSM<RatStates> _fsm;

    public RatAttackState(FSM<RatStates> fsm, RatController rat)
    {
        _rat = rat;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        GameManager.GetAudioManager().PlayClipByName("ratSqueak");
        _rat.StartAttack();
    }

    public void OnUpdate()
    {
        _rat.Attacking();
        _fsm.ChangeState(RatStates.Following);
    }

    public void OnFixedUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }

}
