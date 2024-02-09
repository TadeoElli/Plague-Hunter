using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteAttackState : IState
{
    Brute _brute;


    FSM<BruteStates> _fsm;

    public BruteAttackState(FSM<BruteStates> fsm, Brute brute)
    {
        _brute = brute;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        _brute.animator.SetTrigger("Attack");
        GameManager.GetAudioManager().PlayClipByName("BruteAttack");
        _brute.Invoke("SetBoolCanAttack", _brute.attackCooldown);
    }

    public void OnUpdate()
    {
        if(_brute.canAttack)
        {
            if(!_brute.player.isStealth)
            {
                _fsm.ChangeState(BruteStates.Following);
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
