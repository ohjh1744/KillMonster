using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.IO;
using System.Text;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    [SerializeField] private MissionData[] _missions;
    [SerializeField] private Dictionary<int, MissionData> _dataDic;
    [SerializeField] private SaveData _saveData;

    private int _currentMissionNum;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _currentMissionNum = 1;
        for(int i = 0; i < _missions.Length; i++)
        {
            _dataDic.Add(i+1, _missions[i]);
        }
    }

    public void Save()
    {
        StringBuilder path = new StringBuilder();
        path.Append(Application.dataPath).Append("/Save");
        if (Directory.Exists(path.ToString()) == false)
        {
            Directory.CreateDirectory(path.ToString());
        }
        string json = JsonUtility.ToJson(_saveData);
        File.WriteAllText($"{path}/save.txt", json);
    }

    public void Load()
    {
        StringBuilder path = new StringBuilder();
        path.Append(Application.dataPath).Append("/Save/save.txt");
        if (File.Exists(path.ToString()) == false)
        {
            Debug.Log("불러올 세이브  파일 없음");
            return;
        }
        string json = File.ReadAllText(path.ToString());
        _saveData = JsonUtility.FromJson<SaveData>(json);
        _currentMissionNum = _saveData.MissionNum;

    }
}