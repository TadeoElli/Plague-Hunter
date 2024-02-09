using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleChargeState : IState
{
    BossEnemy _boss;

    float timer;

    FSM<BossStates> _fsm;

    public BossIdleChargeState(FSM<BossStates> fsm, BossEnemy boss)
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
        if(timer > 2.9f)
        {
            if(_boss.player.isStealth)
            {
                _fsm.ChangeState(BossStates.Idle);
            }
            else
            {
                _boss.charges++;
                _fsm.ChangeState(BossStates.Charging);
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
