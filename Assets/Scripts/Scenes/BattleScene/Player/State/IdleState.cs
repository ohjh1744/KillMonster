using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateMachine;

public class IdleState : MovementState
{
    public IdleState(PlayerStateMachine player): base(player)
    {
    }

    public override void Enter()
    {
        Debug.Log("현재 Idle State에 진입!");
    }

    public override void Update()
    {
        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Player.ChangeMovementState(Player.MovementStates[(int)EMovementState.Walk]);
        }
    }

    public override void Exit()
    {
        Debug.Log("Idle State에서 나감!");
    }
}
