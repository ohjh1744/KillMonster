using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum BossSound {Walk, Upset, SecondAttack, ThirdAttack, FourthAttack}
public class BossData : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField]private float _maxhp;
    public float Speed;
    public int BasicDamage;
    public NavMeshAgent NavMesh;
    public GameObject Player;
    public Animator Anim;
    [Header("Upset")]
    public float UpsetTime;
    [HideInInspector] public bool IsUpset;

    public AudioClip[] AudioClips;

    public float Hp { get { return _hp; } set { _hp = value; OnHpChanged?.Invoke(); } }
    public float MaxHp { get { return _maxhp; } set { _maxhp = value;} }
    public UnityAction OnHpChanged;
}
