using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BossData : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField]private float _maxhp;
    public int Gold;
    public float Speed;
    public int BasicDamage;
    [Header("Upset")]
    public float UpsetTime;
    [HideInInspector] public bool IsUpset;
    [Header("Dead")]
    public float DeadTime;

    public float Hp { get { return _hp; } set { _hp = value; OnHpChanged?.Invoke(); } }
    public float MaxHp { get { return _maxhp; } set { _maxhp = value;} }
    public UnityAction OnHpChanged;
}
