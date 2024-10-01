using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1IdleState : BossState
{
    private Boss1StateMachine _boss;

    private BossData _bossData;
    public Boss1IdleState(Boss1StateMachine boss)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
    }
    public override void Enter()
    {
        Debug.Log("BossIdleState�� ����");
    }

    public override void Update()
    {
        //���߿� if������ ������ �����ϸ� �����̵��� ó��
        _boss.ChangeState(_boss.BossStates[(int)EBossState.Move]);
    }
    public override void Exit()
    {
        Debug.Log("BossIdleState�� ����");
    }

}
