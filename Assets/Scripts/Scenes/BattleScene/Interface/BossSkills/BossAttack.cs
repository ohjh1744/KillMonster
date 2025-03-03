using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackType {BaseAttack, UsingSpeedAttack, UsingAnimAttack}
public abstract class BossAttack : MonoBehaviour
{
    [SerializeField] private bool _isAttack;
    public bool IsAttack { get { return _isAttack; } set { _isAttack = value; } }

    [SerializeField] private float _attackDistance;
    public float AttackDistance { get { return _attackDistance; } private set { } }

    [SerializeField] private AttackType _attackType;
    public AttackType AttackType { get { return _attackType; } private set { } }

    private Transform _target;
    public Transform Target { get { return _target; } set { _target = value; } }

    public abstract void StopAttack();

    public virtual void Attack(float damage) { }

    public virtual void Attack(float basicSpeed, float basicDamage) { }

    public virtual void Attack(float bossDamage, int animHash) { }
}
