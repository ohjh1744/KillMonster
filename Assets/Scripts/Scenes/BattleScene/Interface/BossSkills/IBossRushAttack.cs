using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossRushAttack 
{
    public bool IsAttack { get; set; }
    public void Attack(float basicSpeed, float basicDamage);
    public void StopAttack();

}
