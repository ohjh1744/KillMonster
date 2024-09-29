using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public enum FireWeapon { 주총, 보조총 }; // 이들은 상태가 따로 존재. Scene View에서 true false를 각각해줘야함.
public enum NotFireWeapon { 칼, 수류탄 };
public enum Zoom { 줌아웃, 줌인 };

public enum Sound { 걷기, 뛰기, 총교체, 피격};

public class PlayerData : MonoBehaviour
{
    [SerializeField] private float _hp;
    [HideInInspector] public float Hp { get { return _hp; } set { _hp = value; OnHpChanged?.Invoke(); } }
    private float _damage;
    [HideInInspector] public float Damage { get { return _damage; } set { _damage = value; } }
    public float Speed;
    [SerializeField] private float _runGage;
    public float RunMaxGage;
    [HideInInspector] public bool IsRun;
    [HideInInspector]public float RunGage { get { return _runGage; } set { _runGage = value; OnRunGageChanged?.Invoke(); } }

    public float StiffnessTime;

    public Camera Camera;
    public CinemachineVirtualCamera PlayerVirtualCamera;
    public FireWeapon CurFireWeapon;
    public Zoom IsZoom;
    [HideInInspector] public bool IsDamage;
    [HideInInspector] public bool IsSafe;
    [HideInInspector] public bool IsChangeFireWeapon;
    public float ChangeWeaponTime;
    // 들고있는 총 상태, 총기류들, 총기류가 아닌 무기들
    public GameObject[] FireStates;
    public GameObject[] FireWeapons;
    public GameObject[] NotFireWeapons;

    //Aim이미지
    public GameObject[] Aims;

    //총기가 아닌 무기들 공격위치
    public GameObject[] NotFireAttackPos;

    // Fire총기류들 현재 탄약개수, 수류탄개수
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

    [SerializeField] public AudioClip[] AudioClips;
    [SerializeField] public float[] AudioTimes;

    public UnityAction OnHpChanged;
    public UnityAction OnAmmosChanged;
    public UnityAction OnNumGrenadeChanged;
    public UnityAction OnRunGageChanged;

}
