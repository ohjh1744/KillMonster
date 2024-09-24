using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateMachine;

public class IdleState : MovementState
{
    private PlayerStateMachine _player;
    public IdleState(PlayerStateMachine player)
    {
        _player = player;
    }
    public override void Enter()
    {
        Debug.Log("현재 Idle State에 진입!");
    }

    public override void Update()
    {
        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            _player.ChangeMovementState(_player.MovementStates[(int)EMovementState.Walk]);
        }
    }

    public override void Exit()
    {
        Debug.Log("Idle State에서 나감!");
    }
}
