using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossHitAttack 
{
    public bool IsAttack { get; set; }
    public void Attack(int bossDamage);
    public void StopAttack();

}
