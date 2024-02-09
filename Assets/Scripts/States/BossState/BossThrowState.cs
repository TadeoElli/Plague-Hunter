using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowState : IState
{
    BossEnemy _boss;

    FSM<BossStates> _fsm;

    public BossThrowState(FSM<BossStates> fsm, BossEnemy boss)
    {
        _boss = boss;

        _fsm = fsm;

    }

    public void OnEnter()
    {

        _boss.animator.SetBool("IsThrowing", true);
        
    }

    public void OnUpdate()
    {
        
        if(Vector3.Distance(_boss.transform.position, _boss.target.position) <= _boss.chargeDistance && !_boss.player.isStealth)
        {
            if(Vector3.Distance(_boss.transform.position, _boss.target.position) <= _boss.jumpDistance && _boss.hasJumpAttack && _boss.jumps < 3 )
            {
                _boss.animator.SetTrigger("IsJumping");
            }
            else if(_boss.hasChargeAttack && _boss.charges < 3 )
            {
                _boss.charges++;
                _fsm.ChangeState(BossStates.Charging);
            }
        }

    }

    public void OnFixedUpdate()
    {
        if(_boss.player.isStealth)
        {
            _boss.animator.SetBool("IsThrowing", false);
        }
        else
        {
            _boss.transform.LookAt(_boss.target.position);
            _boss.animator.SetBool("IsThrowing", true);
        }
        
    }

    public void OnExit()
    {
        _boss.previewRock.SetActive(false);
        _boss.animator.SetBool("IsThrowing", false);
    }
}
