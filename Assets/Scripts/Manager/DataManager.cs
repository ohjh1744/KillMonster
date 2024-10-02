using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.IO;
using System.Text;
using UnityEngine.UI;
using TMPro;

public enum ESaveState { Before, alreadySave, Success, Fail}
public enum ELoadState { Before, Success, Fail }
public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    [SerializeField] private SaveData _saveData;
    public SaveData SaveData { get { return _saveData; } private set { } }
    public ESaveState SaveState { get; set; }
    public ELoadState LoadState { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ClearGameData();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ClearGameData()
    {
        for (int i = 0; i < SaveData.GameData.Stats.Length; i++)
        {
            SaveData.GameData.Stats[i].CurrentStat = SaveData.GameData.BaseStats[i].CurrentStat;
            SaveData.GameData.Stats[i].UpGradeStat = SaveData.GameData.BaseStats[i].UpGradeStat;
            SaveData.GameData.Stats[i].UpGradeStatGold = SaveData.GameData.BaseStats[i].UpGradeStatGold;
            SaveData.GameData.Stats[i].Level = SaveData.GameData.BaseStats[i].Level;
        }

        for(int i = 0; i < SaveData.GameData.ClearMissions.Length; i++)
        {
            SaveData.GameData.ClearMissions[i] = false;
        }
        SaveData.Gold = 0;
    }


    public void Save(string saveName)
    {
        StringBuilder path = new StringBuilder();
        path.Append(Application.dataPath).Append("/Save");
        if (Directory.Exists(path.ToString()) == false)
        {
            Directory.CreateDirectory(path.ToString());
        }

        string json = JsonUtility.ToJson(SaveData.GameData);
        File.WriteAllText($"{path}/{saveName}.txt", json);
        SaveState = ESaveState.Success;
    }

    public void Load(string loadName)
    {
        StringBuilder path = new StringBuilder();
        path.Append(Application.dataPath).Append($"/Save/{loadName}.txt");
        if (File.Exists(path.ToString()) == false)
        {
            Debug.Log("Cant Find Save File");
            LoadState = ELoadState.Fail;
            return;
        }
        string json = File.ReadAllText(path.ToString());
        SaveData.GameData = JsonUtility.FromJson<GameData>(json);
        Debug.Log("Comoplete");
        LoadState = ELoadState.Success;

    }
}