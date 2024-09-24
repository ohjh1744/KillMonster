using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public enum FireWeapon { ����, ������ }; // �̵��� ���°� ���� ����. Scene View���� true false�� �����������.
public enum NotFireWeapon { Į, ����ź };
public enum Zoom { �ܾƿ�, ���� };

public class PlayerData : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _maxhp;
    [HideInInspector]public float Hp { get { return _hp; } set { _hp = value; OnPlayerHpChanged?.Invoke(); } }
    [HideInInspector]public float MaxHp { get { return _maxhp; } set { _maxhp = value; } }

    public float Speed;
    public float StiffnessTime;
    public Camera Camera;
    public CinemachineVirtualCamera PlayerVirtualCamera;
    public FireWeapon CurFireWeapon;
    public Zoom IsZoom;
    [HideInInspector]public bool IsDamage;
    [HideInInspector]public bool IsSafe; 

    // ����ִ� �� ����, �ѱ����, �ѱ���� �ƴ� �����
    public GameObject[] FireStates;
    public GameObject[] FireWeapons;
    public GameObject[] NotFireWeapons;

    //Aim
    public GameObject[] Aims;

    //�ѱⰡ �ƴ� ����� ������ġ
    public GameObject[] NotFireAttackPos;

    // Fire�ѱ���� ���� ź�ళ��, ����ź����
    public int[] Ammos;
    public int NumGrenade;

    // �� ���⺰ ���������� ������ �ð� -> �����ֱ�üũ������ ����, ���簡�ƴ� �ѹ������������� �����ֱⰡ �����ϵ����ϱ�����.
    [HideInInspector]public float[] FireLastAttackTime;
    [HideInInspector] public float[] NotFireLastAttackTime;


    public UnityAction OnPlayerHpChanged;


}
