using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMove : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] Transform _player;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _moveClip;
    [SerializeField] NavMeshAgent _navMesh;

    private int _moveHash = Animator.StringToHash("Walk");
    private float _curMoveSoundTime;

    public void PlayMoveSound()
    {
        if (Time.time - _curMoveSoundTime > _moveClip.length / _moveClip.length)
        {
            _audioSource.PlayOneShot(_moveClip);
            _curMoveSoundTime = Time.time;
        }
    }
    public void Move()
    {
        _anim.Play(_moveHash);
        _navMesh.SetDestination(_player.position);
    }
}
