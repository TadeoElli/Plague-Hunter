using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFrozenState : IState
{
    BossEnemy _boss;

    FSM<BossStates> _fsm;
    float timer;

    public BossFrozenState(FSM<BossStates> fsm, BossEnemy boss)
    {
        _boss = boss;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        _boss.iceCage.SetActive(true);
        _boss.animator.enabled = false;
        timer = 0;
    }

    public void OnUpdate()
    {
        if(timer > 5f)
        {
            _fsm.ChangeState(BossStates.Idle);
        }
        timer = timer + 1 *Time.deltaTime;
    }

    public void OnFixedUpdate()
    {

        
    }

    public void OnExit()
    {
        _boss.iceCage.SetActive(false);
        _boss.animator.enabled = true;
    }
}
