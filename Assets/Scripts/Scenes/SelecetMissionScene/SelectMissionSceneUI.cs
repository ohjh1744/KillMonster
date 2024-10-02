using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMissionSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject[] missions;

    [SerializeField] private GameObject[] _huntImages;

    [SerializeField] private GameObject[] _clearImages;

    private Dictionary<int, GameObject> _missionDic = new Dictionary<int, GameObject>();

    private SaveData _saveData;

    public void Awake()
    {
        for (int i = 0; i < missions.Length; i++)
        {
            _missionDic.Add(i + 1, missions[i]);
        }

        _saveData = DataManager.Instance.SaveData;

        for(int i = 0; i < _saveData.GameData.ClearMissions.Length; i++)
        {
            if (_saveData.GameData.ClearMissions[i] == true)
            {
                _huntImages[i].SetActive(false);
                _clearImages[i].SetActive(true);
            }
            else
            {
                _huntImages[i].SetActive(true);
                _clearImages[i].SetActive(false);
            }
        }
    }

    public void OnActiveMission(int num)
    {
        if (_missionDic[num].activeSelf == true)
        {
            _missionDic[num].SetActive(false);
        }
        else
        {
            _missionDic[num].SetActive(true);
        }
    }
}
