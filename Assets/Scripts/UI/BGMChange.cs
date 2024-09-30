using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChange : MonoBehaviour
{
    [SerializeField] AudioClip _bgm;
    private void Start()
    {
        SoundManager.Instance.PlayeBGM(_bgm);
    }
}
