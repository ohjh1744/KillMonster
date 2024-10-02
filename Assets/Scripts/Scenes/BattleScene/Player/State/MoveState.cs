using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateMachine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MoveState : MovementState
{
    private PlayerStateMachine _player;

    private PlayerData _playerData;

    private Animator _anim;

    private AudioClip _walkClip;

    private AudioClip _runClip;

    private Rigidbody _rigid;

    private Vector3 _moveDir;

    private float _originSpeed;

    private bool _isRun;

    private bool _hasStoppedRunSound;

    private float _cantMoveTime;

    private float _curCantMoveTime;

    private float _walkSoundTime;

    private float _curWalkSoundTime;

    private float _runSoundTime;

    private float _curRunSoundTime;

    private AudioSource _audioSource;

    public MoveState(PlayerStateMachine player)
    {
        _player = player;
        _playerData = _player.PlayerData;
        _originSpeed = _playerData.Speed;
        _cantMoveTime = _playerData.StiffnessTime;
        _rigid = _player.Rigid;
        _audioSource = _player.MovementStateAudio;
        _walkSoundTime = _player.AudioTimes[(int)ESound.Walk];
        _runSoundTime = _player.AudioTimes[(int)ESound.Run];
        _walkClip = _player.AudioClips[(int)ESound.Walk];
        _runClip = _player.AudioClips[(int)ESound.Run];
    }

    public override void Enter()
    {
        Debug.Log("현재 Walk State에 진입!");
        _anim = _playerData.FireStates[(int)_playerData.CurFireWeapon].GetComponent<Animator>();
        _curWalkSoundTime = 0;
        _curRunSoundTime = 0;
        _isRun = false;
        _hasStoppedRunSound = true;
    }

    public override void Update()
    {
        if (_playerData.IsChangeFireWeapon == true)
        {
            _anim = _playerData.FireStates[(int)_playerData.CurFireWeapon].GetComponent<Animator>();
        }

        PlayWalkSound();
        PlayRunSound();
        if (_playerData.IsDamage == true)
        {
            CantWalk();
        }
        else if(_playerData.IsDamage == false)
        {
            Walk();
            Run();
        }

        if (_moveDir == Vector3.zero)
        {
            _player.ChangeMovementState(_player.MovementStates[(int)EMovementState.Idle]);
        }
    }

    public override void Exit()
    {
        Debug.Log("Walk State에서 나감!");
        _audioSource.Stop();
    }

    private void PlayWalkSound()
    {
        if (Time.time - _curWalkSoundTime > _walkSoundTime && _isRun == false)
        {
            _audioSource.PlayOneShot(_walkClip);
            _curWalkSoundTime = Time.time;
        }
    }
    private void Walk()
    {
        _moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 dir = _player.transform.rotation * _moveDir * _playerData.Speed;
        dir.y = 0f;

        _rigid.velocity = dir;
    }
    private void CantWalk()
    {
        _curCantMoveTime += Time.deltaTime;
        if (_curCantMoveTime > _cantMoveTime)
        {
            _playerData.IsDamage = false;
            _curCantMoveTime = 0;
        }
    }

    private void PlayRunSound()
    {
        if (Time.time - _curRunSoundTime > _runSoundTime && _isRun == true)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(_runClip);
            _curRunSoundTime = Time.time;
            _hasStoppedRunSound = false;
        }
        if(_isRun == false && _hasStoppedRunSound == false)
        {
            _audioSource.Stop();
            _hasStoppedRunSound = true;
            _curWalkSoundTime = 0;
        }
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (_playerData.RunGage > 0)
            {
                _isRun = true;
                _playerData.IsRun = true;
                _playerData.RunGage -= Time.deltaTime;
                _playerData.Speed = _originSpeed * 2;
                _anim.SetBool("isRun", true);
            }
            else
            {
                CantRun();
            }
        }
        else
        {
            CantRun();
            _playerData.IsRun = false;
        }
    }

    private void CantRun()
    {
        _isRun = false;
        _playerData.Speed = _originSpeed;
        _anim.SetBool("isRun", false);
    }
}
