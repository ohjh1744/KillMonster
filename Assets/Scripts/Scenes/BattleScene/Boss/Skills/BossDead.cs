using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDead : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _deadClip;

    [SerializeField] private CinemachineVirtualCamera _bossCamera;

    [SerializeField] private int priorityCamera;

    private float _deadTime;

    private float _currentTime;

    public void Dead()
    {
        _bossCamera.Priority = priorityCamera;
        _audioSource.Stop();
        _audioSource.clip = _deadClip;
        _audioSource.Play();
    }
}
