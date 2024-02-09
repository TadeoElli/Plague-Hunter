using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleJumpState : IState
{
    BossEnemy _boss;

    float timer;

    FSM<BossStates> _fsm;

    public BossIdleJumpState(FSM<BossStates> fsm, BossEnemy boss)
    {
        _boss = boss;
        _fsm = fsm;
    }

    public void OnEnter()
    {
        timer = 0f;
    }

    public void OnUpdate()
    {
        if(timer > 1.5f)
        {
            if(_boss.player.isStealth)
            {
                _fsm.ChangeState(BossStates.Idle);
            }
            else
            {
                _boss.animator.SetTrigger("IsJumping");
            }
            
        }
        timer = timer + 1 *Time.deltaTime;
    }

    public void OnFixedUpdate()
    {
        
    }
    public void OnExit()
    {
        
    }
}
