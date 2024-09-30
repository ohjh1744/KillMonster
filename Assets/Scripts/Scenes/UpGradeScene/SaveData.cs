using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EMission {����ŷ, ����ŷ, �𵥵�ŷ }
public enum EStatType {������, �ִ�Hp }

[System.Serializable]
public class GameData
{
    public bool[] ClearMissions;
    public Stat[] Stat;
    public BaseStat[] BaseStat;
    public int Gold;
}

[CreateAssetMenu( menuName = "ScriptableObjects/SaveData")]
public class SaveData : ScriptableObject
{
    public  GameData GameData;

    public float CurrentDamage { get { return GameData.Stat[(int)EStatType.������].CurrentStat; } set { GameData.Stat[(int)EStatType.������].CurrentStat = value;} }
    public float CurrentMaxHp { get { return GameData.Stat[(int)EStatType.�ִ�Hp].CurrentStat; } set { GameData.Stat[(int)EStatType.�ִ�Hp].CurrentStat = value; } }
    public float Damage { get { return GameData.Stat[(int)EStatType.������].UpGradeStat; } set { GameData.Stat[(int)EStatType.������].UpGradeStat = value; OnUpGradeDamageChanged?.Invoke(); } }
    public float MaxHp {  get { return GameData.Stat[(int)EStatType.�ִ�Hp].UpGradeStat; } set { GameData.Stat[(int)EStatType.�ִ�Hp].UpGradeStat = value; OnUpGradeMaxHpChagned?.Invoke(); } }
    public int Gold { get { return GameData.Gold; } set { GameData.Gold = value; OnGoldChanged?.Invoke(); } }
    public int UpGradeDamageGold { get { return GameData.Stat[(int)EStatType.������].UpGradeStatGold; } set { GameData.Stat[(int)EStatType.������].UpGradeStatGold = value; OnUpGradeDamageGoldChanged?.Invoke(); } }
    public int UpGradeMaxHpGold { get { return GameData.Stat[(int)EStatType.�ִ�Hp].UpGradeStatGold; } set { GameData.Stat[(int)EStatType.�ִ�Hp].UpGradeStatGold = value; OnUpGradeMaxHpGoldChanged?.Invoke(); } }
    public int UpGradeDamageLevel { get { return GameData.Stat[(int)EStatType.������].Level; } set { GameData.Stat[(int)EStatType.������].Level = value; OnUpGradeDmaageLevel?.Invoke(); } }
    public int UpGradeMaxHpLevel { get { return GameData.Stat[(int)EStatType.�ִ�Hp].Level; } set { GameData.Stat[(int)EStatType.�ִ�Hp].Level = value; OnUpGradeMaxHpLevel?.Invoke(); } }
    public int DamageMaxLevel {  get { return GameData.Stat[(int)EStatType.������].MaxLevel; } set { GameData.Stat[(int)EStatType.������].MaxLevel = value; } }
    public int MaxHpMaxLevel { get { return GameData.Stat[(int)EStatType.�ִ�Hp].MaxLevel; } set { GameData.Stat[(int)EStatType.�ִ�Hp].MaxLevel = value; } }

    public UnityAction OnUpGradeDamageChanged;
    public UnityAction OnUpGradeMaxHpChagned;
    public UnityAction OnGoldChanged;
    public UnityAction OnUpGradeDamageGoldChanged;
    public UnityAction OnUpGradeMaxHpGoldChanged;
    public UnityAction OnUpGradeDmaageLevel;
    public UnityAction OnUpGradeMaxHpLevel;
}
