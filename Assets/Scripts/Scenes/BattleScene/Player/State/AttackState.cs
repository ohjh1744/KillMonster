using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : IState
{
    private PlayerStateMachine _player;
    public PlayerStateMachine Player { get { return _player; } set { _player = value; } }
    public AttackState(PlayerStateMachine player)
    {
        _player = player;
    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
