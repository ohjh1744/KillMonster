using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    private BossStateMachine _boss;

    private BossData _bossData;
    public BossIdleState(BossStateMachine boss)
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
