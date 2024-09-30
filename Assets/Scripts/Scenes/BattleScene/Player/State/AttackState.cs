using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : IState
{
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
    public virtual void FixedUpdate() { }

}
