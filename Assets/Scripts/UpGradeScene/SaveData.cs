using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveData : MonoBehaviour
{
    [HideInInspector] public int MissionNum;
    [SerializeField] private Stat DamageState;
    [SerializeField] private Stat MaxHpState;
    [SerializeField] private int _gold;

    public float Damage { get { return DamageState.UpGradeStat; } set { DamageState.UpGradeStat = value; OnUpGradeDamageChanged?.Invoke(); } }
    public float MaxHp {  get { return MaxHpState.UpGradeStat; } set { MaxHpState.UpGradeStat = value; OnUpGradeMaxHpChagned?.Invoke(); } }
    public int Gold { get { return _gold; } set { _gold = value; OnGoldChanged?.Invoke(); } }
    public int UpGradeDamageGold { get { return DamageState.UpGradeStatGold; } set { DamageState.UpGradeStatGold = value; OnUpGradeDamageGoldChanged?.Invoke(); } }
    public int UpGradeMaxHpGold { get { return MaxHpState.UpGradeStatGold; } set { MaxHpState.UpGradeStatGold = value; OnUpGradeMaxHpGoldChanged?.Invoke(); } }
    public int UpGradeDamageLevel { get { return DamageState.Level; } set { DamageState.Level = value; OnUpGradeDmaageLevel?.Invoke(); } }
    public int UpGradeMaxHpLevel { get { return MaxHpState.Level; } set { MaxHpState.Level = value; OnUpGradeMaxHpLevel?.Invoke(); } }
    public int DamageMaxLevel {  get { return DamageState.MaxLevel; } set { DamageState.MaxLevel = value; } }
    public int MaxHpMaxLevel { get { return MaxHpState.MaxLevel; } set { MaxHpState.MaxLevel = value; } }

    public UnityAction OnUpGradeDamageChanged;
    public UnityAction OnUpGradeMaxHpChagned;
    public UnityAction OnGoldChanged;
    public UnityAction OnUpGradeDamageGoldChanged;
    public UnityAction OnUpGradeMaxHpGoldChanged;
    public UnityAction OnUpGradeDmaageLevel;
    public UnityAction OnUpGradeMaxHpLevel;
}
