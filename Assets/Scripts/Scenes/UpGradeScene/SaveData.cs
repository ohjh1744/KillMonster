using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EMission {데몬킹, 좀비킹, 언데드킹 }
public enum EStatType {데미지, 최대Hp }

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

    public float CurrentDamage { get { return GameData.Stat[(int)EStatType.데미지].CurrentStat; } set { GameData.Stat[(int)EStatType.데미지].CurrentStat = value;} }
    public float CurrentMaxHp { get { return GameData.Stat[(int)EStatType.최대Hp].CurrentStat; } set { GameData.Stat[(int)EStatType.최대Hp].CurrentStat = value; } }
    public float Damage { get { return GameData.Stat[(int)EStatType.데미지].UpGradeStat; } set { GameData.Stat[(int)EStatType.데미지].UpGradeStat = value; OnUpGradeDamageChanged?.Invoke(); } }
    public float MaxHp {  get { return GameData.Stat[(int)EStatType.최대Hp].UpGradeStat; } set { GameData.Stat[(int)EStatType.최대Hp].UpGradeStat = value; OnUpGradeMaxHpChagned?.Invoke(); } }
    public int Gold { get { return GameData.Gold; } set { GameData.Gold = value; OnGoldChanged?.Invoke(); } }
    public int UpGradeDamageGold { get { return GameData.Stat[(int)EStatType.데미지].UpGradeStatGold; } set { GameData.Stat[(int)EStatType.데미지].UpGradeStatGold = value; OnUpGradeDamageGoldChanged?.Invoke(); } }
    public int UpGradeMaxHpGold { get { return GameData.Stat[(int)EStatType.최대Hp].UpGradeStatGold; } set { GameData.Stat[(int)EStatType.최대Hp].UpGradeStatGold = value; OnUpGradeMaxHpGoldChanged?.Invoke(); } }
    public int UpGradeDamageLevel { get { return GameData.Stat[(int)EStatType.데미지].Level; } set { GameData.Stat[(int)EStatType.데미지].Level = value; OnUpGradeDmaageLevel?.Invoke(); } }
    public int UpGradeMaxHpLevel { get { return GameData.Stat[(int)EStatType.최대Hp].Level; } set { GameData.Stat[(int)EStatType.최대Hp].Level = value; OnUpGradeMaxHpLevel?.Invoke(); } }
    public int DamageMaxLevel {  get { return GameData.Stat[(int)EStatType.데미지].MaxLevel; } set { GameData.Stat[(int)EStatType.데미지].MaxLevel = value; } }
    public int MaxHpMaxLevel { get { return GameData.Stat[(int)EStatType.최대Hp].MaxLevel; } set { GameData.Stat[(int)EStatType.최대Hp].MaxLevel = value; } }

    public UnityAction OnUpGradeDamageChanged;
    public UnityAction OnUpGradeMaxHpChagned;
    public UnityAction OnGoldChanged;
    public UnityAction OnUpGradeDamageGoldChanged;
    public UnityAction OnUpGradeMaxHpGoldChanged;
    public UnityAction OnUpGradeDmaageLevel;
    public UnityAction OnUpGradeMaxHpLevel;
}
