using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public enum EFireWeapon { Main, Sub }; // 이들은 상태가 따로 존재. Scene View에서 true false를 각각해줘야함.
public enum ENotFireWeapon { Knife, Grenade };
public enum EZoom { ZoomOut, ZoomIn };
public enum ESound { Walk, Run, ChangeFireWeapon, TakeDamage};

public class PlayerData : MonoBehaviour
{
    [Header("Basic Stat")]
    [SerializeField] private float _hp;
    public float Hp { get { return _hp; } set { _hp = value; OnHpChanged?.Invoke(); } }
    public bool IsSafe { get; set; }
    public bool IsDamage { get; set; }

    private float _damage;
    public float Damage { get { return _damage; } set { _damage = value; } }

    [SerializeField] private float _speed;
    public float Speed { get { return _speed; } set { _speed = value; } }

    public bool IsRun { get; set; }

    [SerializeField] private float _runGage;
    public float RunGage { get { return _runGage; } set { _runGage = value; OnRunGageChanged?.Invoke(); } }

    [SerializeField] private float _runMaxGage;
    public float RunMaxGage { get { return _runMaxGage; } private set { } }

    [SerializeField] private float _stiffnessTime;
    public float StiffnessTime { get { return _stiffnessTime; } private set { } }

    //------------------------------------------------------
    [Header("Weapon")]

    [SerializeField] private float _changeWeaponTime;
    public float ChangeWeaponTime { get; private set; }
    public EFireWeapon CurFireWeapon { get; set; }
    public EZoom IsZoom { get; set; }
    public bool IsChangeFireWeapon { get; set; }

    [SerializeField] private GameObject[] _fireStates;
    public GameObject[] FireStates { get { return _fireStates; } private set { } }

    [SerializeField] private GameObject[] _fireWeapons;
    public GameObject[] FireWeapons{get { return _fireWeapons; } private set { } }

    [SerializeField] private GameObject[] _notFireWeapons;
    public GameObject[] NotFireWeapons { get { return _notFireWeapons; } private set { } }

    [SerializeField] private GameObject[] _notFireAttackPos;
    public GameObject[] NotFireAttackPos { get { return _notFireAttackPos; } private set { } }

    [SerializeField]private int[] Ammos;

    public void SetAmmos(int fireWeapon, int value)
    {
        Ammos[fireWeapon] = value;
        OnAmmosChanged?.Invoke();
    }

    public int GetAmmos(int fireWeapon)
    {
        return Ammos[fireWeapon];
    }

    [SerializeField] public int _numGrenade;
    [HideInInspector] public int NumGrenade { get { return _numGrenade; } set { _numGrenade = value; OnNumGrenadeChanged?.Invoke(); } }

    public UnityAction OnHpChanged;

    public UnityAction OnAmmosChanged;

    public UnityAction OnNumGrenadeChanged;

    public UnityAction OnRunGageChanged;

}
