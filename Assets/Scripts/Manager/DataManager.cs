using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public SaveData SaveData;
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
        for (int i = 0; i < SaveData.GameData.Stat.Length; i++)
        {
            SaveData.GameData.Stat[i].CurrentStat = SaveData.GameData.BaseStat[i].CurrentStat;
            SaveData.GameData.Stat[i].UpGradeStat = SaveData.GameData.BaseStat[i].UpGradeStat;
            SaveData.GameData.Stat[i].UpGradeStatGold = SaveData.GameData.BaseStat[i].UpGradeStatGold;
            SaveData.GameData.Stat[i].Level = SaveData.GameData.BaseStat[i].Level;
            SaveData.GameData.Stat[i].MaxLevel = SaveData.GameData.BaseStat[i].MaxLevel;
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
    }

    public void Load(string loadName)
    {
        StringBuilder path = new StringBuilder();
        path.Append(Application.dataPath).Append($"/Save/{loadName}.txt");
        if (File.Exists(path.ToString()) == false)
        {
            Debug.Log("불러올 세이브  파일 없음");
            return;
        }
        string json = File.ReadAllText(path.ToString());
        SaveData.GameData = JsonUtility.FromJson<GameData>(json);
        Debug.Log("잘불러옴");

    }
}