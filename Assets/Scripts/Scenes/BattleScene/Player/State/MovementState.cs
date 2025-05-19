using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class MovementState : IState
{
    private PlayerStateMachine _player;
    public PlayerStateMachine Player { get { return _player; } set { _player = value; } }
    public MovementState(PlayerStateMachine player)
    {
        _player = player;
    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
