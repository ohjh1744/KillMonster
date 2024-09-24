using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Mission", fileName = "MissionName")]
public class MissionData : ScriptableObject
{
    public string TargetName;
    public int Level;
    public int Compensation;
    public Sprite BossSprite;
    public string Information;
    public string SceneName;
}
