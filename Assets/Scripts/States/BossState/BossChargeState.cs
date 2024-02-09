using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeState : IState
{
    BossEnemy _boss;
    Vector3 newPosition;

    FSM<BossStates> _fsm;

    public BossChargeState(FSM<BossStates> fsm, BossEnemy boss)
    {
        _boss = boss;

        _fsm = fsm;

    }

    public void OnEnter()
    {   
        _boss.chargeMeleeSensor.TurnOn();
        _boss.animator.SetBool("IsBashing", true);
        _boss.transform.LookAt(_boss.target.position);
        GameManager.GetAudioManager().PlayClipByName("BossCharge");
        newPosition = _boss.transform.position + _boss.transform.forward * 20f;

    }

    public void OnUpdate()
    {
        if(Vector3.Distance(_boss.transform.position, newPosition) < 1f)
        {
            if(_boss.player.isStealth)
            {
                _fsm.ChangeState(BossStates.Idle);
                _boss.charges = 0;
                _boss.hasChargeAttack = false;
                _boss.SetChargeTimer();
            }
            else
            {
                if(_boss.charges >= 3)
                {
                    _fsm.ChangeState(BossStates.Idle);
                    _boss.charges = 0;
                    _boss.hasChargeAttack = false;
                    _boss.SetChargeTimer();
                }
                else
                {
                    _fsm.ChangeState(BossStates.IdleCharge);
                }
            }
        }
    }

    public void OnFixedUpdate()
    {
        _boss.transform.position = Vector3.MoveTowards(_boss.transform.position,newPosition, _boss.speed * Time.deltaTime);
        /*_boss.transform.position = _boss.transform.position + _boss.transform.forward * _boss.speed * Time.deltaTime;
        Vector3 aux = _boss.transform.position;
        aux.y = 0f;
        _boss.transform.position = aux;
        */
        //_boss.transform.LookAt(newPosition);
    }

    public void OnExit()
    {
        GameManager.GetAudioManager().StopClipByName("BossCharge");
        _boss.chargeMeleeSensor.TurnOff();
        _boss.animator.SetBool("IsBashing", false);
    }

}
