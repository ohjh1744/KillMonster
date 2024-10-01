using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class MovementState : IState
{
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
