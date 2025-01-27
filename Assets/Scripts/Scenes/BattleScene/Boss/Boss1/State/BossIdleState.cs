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
        Debug.Log("BossIdleState에 진입");
    }

    public override void Update()
    {
        //나중에 if문으로 게임이 시작하면 움직이도록 처리
        _boss.ChangeState(_boss.BossStates[(int)EBossState.Move]);
    }
    public override void Exit()
    {
        Debug.Log("BossIdleState에 나감");
    }

}
