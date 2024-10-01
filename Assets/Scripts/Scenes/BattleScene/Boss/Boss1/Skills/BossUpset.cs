using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossUpset : MonoBehaviour
{

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _upsetClip;


    private int _moveHash = Animator.StringToHash("Walk");
    private float _curMoveSoundTime;

    public void TurnUpset(BossData bossData)
    {
        _audioSource.clip = _upsetClip;
        _audioSource.Play();
        bossData.IsUpset = true;
        bossData.Damage *= 2;
        bossData.Speed *= 2;
    }
}
