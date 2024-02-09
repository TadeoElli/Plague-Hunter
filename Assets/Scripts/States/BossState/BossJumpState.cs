using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpState : IState
{
    BossEnemy _boss;

    FSM<BossStates> _fsm;

    private float journeyTime, timer;
    private float speed = 0.18f;

    Transform startPos;
    Vector3  endPos, centerPoint, startRelCenter, endRelCenter;

	

    public BossJumpState(FSM<BossStates> fsm, BossEnemy boss)
    {
        _boss = boss;

        _fsm = fsm;

    }

    public void OnEnter()
    {   
        journeyTime = 1.5f / 2;
        startPos = _boss.transform;
        timer = 0;
        _boss.transform.LookAt(_boss.target.position);
        
        if(Vector3.Distance(_boss.transform.position, _boss.target.position) > _boss.jumpDistance)
        {
            endPos = _boss.transform.position + _boss.transform.forward * 10;
        }
        else
        {
            endPos = _boss.target.position;
        }
        
    }

    public void OnUpdate()
    {
        _boss.transform.forward = _boss.transform.position + _boss.target.position;
        timer = timer + 1 * Time.deltaTime;
    }

    public void OnFixedUpdate()
    {
        GetCenter(Vector3.up);
        float fracComplete = timer / journeyTime * speed;
        _boss.transform.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete * speed);
        _boss.transform.position += centerPoint;
        if(Vector3.Distance(_boss.transform.position, endPos) < 1f)
        {
            if(_boss.player.isStealth)
            {
                _boss.SetJumpTimer();
                _boss.jumps = 0;
                _boss.hasJumpAttack = false;
                _boss.transform.position = endPos;
                _fsm.ChangeState(BossStates.Idle);
            }
            else
            {
                if(_boss.jumps >= 3)
                {
                    _boss.SetJumpTimer();
                    _boss.jumps = 0;
                    _boss.hasJumpAttack = false;
                    _boss.transform.position = endPos;
                    _fsm.ChangeState(BossStates.Idle);
                }
                else    
                {
                    _boss.transform.position = endPos;
                    _fsm.ChangeState(BossStates.IdleJump);
                }  
            }
        }
    }

    public void OnExit()
    {
        
    }

    public void GetCenter(Vector3 direction) 
    {
        centerPoint = (startPos.position + endPos) * 0.5f;
        centerPoint -= direction;
        startRelCenter = startPos.position - centerPoint;
        endRelCenter = endPos - centerPoint;
    }


}
