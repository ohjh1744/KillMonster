using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossWorldAreaAttack 
{
    public bool IsAttack { get; set; }

    public void Attack(float bossDamage, int animHash);

    public void StopAttack();

}
