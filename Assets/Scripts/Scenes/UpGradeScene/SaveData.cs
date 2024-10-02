using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public enum EMission {DemonKing, ZombieKing, UnDeadKing }
public enum EStatType {Damage, MaxHp }

[System.Serializable]
public class Stat
{
    [SerializeField] private float _currentStat;
    public float CurrentStat { get { return _currentStat; } set { _currentStat = value; } }

    [SerializeField] private float _upGradeStat;
    public float UpGradeStat { get { return _upGradeStat; } set { _upGradeStat = value; } }

    [SerializeField] private int _upGradeStatGold;
    public int UpGradeStatGold { get { return _upGradeStatGold; } set { _upGradeStatGold = value; } }

    [SerializeField] private int _level;
    public int Level { get { return _level; } set { _level = value; } }

    [SerializeField] private int _maxLevel;
    public int MaxLevel { get { return _maxLevel; } private set { } }

}

[System.Serializable]
public class BaseStat
{
    [SerializeField] private float _currentStat;
    public float CurrentStat { get { return _currentStat; }  private set { } }

    [SerializeField] private float _upGradeStat;
    public float UpGradeStat { get { return _upGradeStat; } private set { } }

    [SerializeField] private int _upGradeStatGold;
    public int UpGradeStatGold { get { return _upGradeStatGold; } private set { } }

    [SerializeField] private int _level;
    public int Level { get { return _level; } private set { } }
}


[System.Serializable]
public class GameData
{
    [SerializeField] private bool[] _clearMissions;
    public bool[] ClearMissions { get { return _clearMissions; } private set { } }

    [SerializeField]private  Stat[] _stats;
    public Stat[] Stats { get { return _stats; } private set { } }

    [SerializeField] private BaseStat[] _baseStats;
    public BaseStat[] BaseStats { get { return _baseStats; } private set { } }

    public int Gold { get; set; }

}

[CreateAssetMenu( menuName = "ScriptableObjects/SaveData")]
public class SaveData : ScriptableObject
{
   [SerializeField] private GameData _gameData;
    public GameData GameData { get { return _gameData; } set { _gameData = value; } }

    public float CurrentDamage { get { return _gameData.Stats[(int)EStatType.Damage].CurrentStat; } set { _gameData.Stats[(int)EStatType.Damage].CurrentStat = value;} }
    public float CurrentMaxHp { get { return _gameData.Stats[(int)EStatType.MaxHp].CurrentStat; } set { _gameData.Stats[(int)EStatType.MaxHp].CurrentStat = value; } }
    public float Damage { get { return _gameData.Stats[(int)EStatType.Damage].UpGradeStat; } set { _gameData.Stats[(int)EStatType.Damage].UpGradeStat = value; OnUpGradeDamageChanged?.Invoke(); } }
    public float MaxHp {  get { return _gameData.Stats[(int)EStatType.MaxHp].UpGradeStat; } set { _gameData.Stats[(int)EStatType.MaxHp].UpGradeStat = value; OnUpGradeMaxHpChagned?.Invoke(); } }
    public int Gold { get { return GameData.Gold; } set { GameData.Gold = value; OnGoldChanged?.Invoke(); } }
    public int UpGradeDamageGold { get { return _gameData.Stats[(int)EStatType.Damage].UpGradeStatGold; } set { _gameData.Stats[(int)EStatType.Damage].UpGradeStatGold = value; OnUpGradeDamageGoldChanged?.Invoke(); } }
    public int UpGradeMaxHpGold { get { return _gameData.Stats[(int)EStatType.MaxHp].UpGradeStatGold; } set { _gameData.Stats[(int)EStatType.MaxHp].UpGradeStatGold = value; OnUpGradeMaxHpGoldChanged?.Invoke(); } }
    public int UpGradeDamageLevel { get { return _gameData.Stats[(int)EStatType.Damage].Level; } set { _gameData.Stats[(int)EStatType.Damage].Level = value; OnUpGradeDmaageLevel?.Invoke(); } }
    public int UpGradeMaxHpLevel { get { return _gameData.Stats[(int)EStatType.MaxHp].Level; } set { _gameData.Stats[(int)EStatType.MaxHp].Level = value; OnUpGradeMaxHpLevel?.Invoke(); } }
    public int DamageMaxLevel { get { return _gameData.Stats[(int)EStatType.Damage].MaxLevel; } private set { } }
    public int MaxHpMaxLevel { get { return _gameData.Stats[(int)EStatType.MaxHp].MaxLevel; }  private set { } }


    public UnityAction OnCurrentDamageChanged;
    public UnityAction OnCurrentMaxhpChanged;
    public UnityAction OnUpGradeDamageChanged;
    public UnityAction OnUpGradeMaxHpChagned;
    public UnityAction OnGoldChanged;
    public UnityAction OnUpGradeDamageGoldChanged;
    public UnityAction OnUpGradeMaxHpGoldChanged;
    public UnityAction OnUpGradeDmaageLevel;
    public UnityAction OnUpGradeMaxHpLevel;
}
