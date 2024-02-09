using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : IState
{
    BossEnemy _boss;
    float timer;

    FSM<BossStates> _fsm;

    public BossIdleState(FSM<BossStates> fsm, BossEnemy boss)
    {
        _boss = boss;

        _fsm = fsm;

    }

    public void OnEnter()
    {
        timer = 0f;
        GameManager.GetAudioManager().PlayClipByName("BossRoar");
    }

    public void OnUpdate()
    {

        if(timer > 2.9f)
        {
            if(Vector3.Distance(_boss.transform.position, _boss.target.position) <= _boss.maxthrowDistance && !_boss.player.isStealth)
            {
                
                if(Vector3.Distance(_boss.transform.position, _boss.target.position) <= _boss.chargeDistance && _boss.hasChargeAttack && Vector3.Distance(_boss.transform.position, _boss.target.position) > _boss.jumpDistance && _boss.charges < 3)
                {
                    _boss.charges++;
                    _fsm.ChangeState(BossStates.Charging);
                }
                else if(Vector3.Distance(_boss.transform.position, _boss.target.position) <= _boss.jumpDistance && _boss.hasJumpAttack && _boss.jumps < 3)
                {
                    
                    _boss.animator.SetTrigger("IsJumping");
                }
                else
                {
                    _fsm.ChangeState(BossStates.Throwing);
                }
            }
        }
        timer = timer + 1 *Time.deltaTime;

    }

    public void OnFixedUpdate()
    {

    }

    public void OnExit()
    {
        GameManager.GetAudioManager().StopClipByName("BossRoar");
    }

}
