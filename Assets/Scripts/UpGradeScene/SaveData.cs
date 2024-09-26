using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveData : MonoBehaviour
{
    public int MissionNum;
    [SerializeField] private float _upGradeDamage;
    [SerializeField] private float _upGradeMaxHp;
    [SerializeField] private int _Gold;
    [SerializeField] private int _upGradeDamageGold;
    [SerializeField] private int _upGradeMaxHpGold;

    public float UpGradeDamage { get { return _upGradeDamage; } set { _upGradeDamage = value; OnUpGradeDamageChanged?.Invoke(); } }
    public float UpGradeMaxHp {  get { return _upGradeMaxHp; } set { _upGradeMaxHp = value; OnUpGradeMaxHpChagned?.Invoke(); } }
    public int Gold { get { return _Gold; } set { _Gold = value; OnGoldChanged?.Invoke(); } }
    public int UpGradeDamageGold { get { return _upGradeDamageGold; } set { _upGradeDamageGold = value; OnUpGradeDamageGoldChanged?.Invoke(); } }
    public int UpGradeMaxHpGold { get { return _upGradeMaxHpGold; } set { _upGradeMaxHpGold = value; OnUpGradeMaxHpGoldChanged?.Invoke(); } }

    public UnityAction OnUpGradeDamageChanged;
    public UnityAction OnUpGradeMaxHpChagned;
    public UnityAction OnGoldChanged;
    public UnityAction OnUpGradeDamageGoldChanged;
    public UnityAction OnUpGradeMaxHpGoldChanged;
}
