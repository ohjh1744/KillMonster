using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public enum FireWeapon { ����, ������ }; // �̵��� ���°� ���� ����. Scene View���� true false�� �����������.
public enum NotFireWeapon { Į, ����ź };
public enum Zoom { �ܾƿ�, ���� };

public enum Sound { �ȱ�, �ٱ�, �ѱ�ü, �ǰ�};

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
    // ����ִ� �� ����, �ѱ����, �ѱ���� �ƴ� �����
    public GameObject[] FireStates;
    public GameObject[] FireWeapons;
    public GameObject[] NotFireWeapons;

    //Aim�̹���
    public GameObject[] Aims;

    //�ѱⰡ �ƴ� ����� ������ġ
    public GameObject[] NotFireAttackPos;

    // Fire�ѱ���� ���� ź�ళ��, ����ź����
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
