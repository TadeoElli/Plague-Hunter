using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : IState
{
    BossEnemy _boss;


    FSM<BossStates> _fsm;

    public BossDeathState(FSM<BossStates> fsm, BossEnemy boss)
    {
        _boss = boss;
        _fsm = fsm;
    }

    public void OnEnter()
    {
        
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
