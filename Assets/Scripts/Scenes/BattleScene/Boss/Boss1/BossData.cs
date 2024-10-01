using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BossData : MonoBehaviour
{
    [Header("Basic Stat")]
    [SerializeField] private float _hp;
    public float Hp { get { return _hp; } set { _hp = value; OnHpChanged?.Invoke(); } }

    [SerializeField] private float _maxhp;
    public float MaxHp { get { return _maxhp; } private set { } }

    [SerializeField] private int _gold;
    public int Gold { get { return _gold; } private set { } }

    [SerializeField] private float _speed;
    public float Speed { get { return _speed; } set { _speed = value; } }

    [SerializeField] private float _damage;
    public float Damage { get { return _damage; } set { _damage = value; } }

    [Header("Upset")]

    [SerializeField] private float _upsetTime;
    public float UpsetTime { get { return _upsetTime; } private set { } }

    public bool IsUpset { get; set; }

    [Header("Dead")]

    [SerializeField] private float _deadTime;
    public float DeadTime { get { return _deadTime; } private set { } }



    public UnityAction OnHpChanged;
}
