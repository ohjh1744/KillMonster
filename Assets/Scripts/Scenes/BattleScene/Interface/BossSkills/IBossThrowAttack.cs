using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossThrowAttack
{
    public Transform Target { get; set; }
    public bool IsAttack { get; set; }
    public void Attack(float bossBasicDamage);
    public void StopAttack();
}
