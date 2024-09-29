using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMission : MonoBehaviour
{
    [SerializeField] private GameObject[] missions;

    private Dictionary<int, GameObject> _missionDic = new Dictionary<int, GameObject>();

    public void Awake()
    {
        for(int i = 0; i < missions.Length; i++)
        {
            _missionDic.Add(i + 1, missions[i]);
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
