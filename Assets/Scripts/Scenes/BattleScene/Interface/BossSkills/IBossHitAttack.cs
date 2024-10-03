using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossHitAttack 
{
    public float AttackDistance { get; }
    public bool IsAttack { get; set; }
    public void Attack(float bossDamage);
    public void StopAttack();

}
