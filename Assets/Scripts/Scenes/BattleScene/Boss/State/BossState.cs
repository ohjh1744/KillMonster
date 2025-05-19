using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : IState
{
    private BossStateMachine _boss;
    public BossStateMachine Boss { get { return _boss; } set { _boss = value; } }

    public BossState(BossStateMachine bossStateMachine)
    {
        _boss = bossStateMachine;
    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
