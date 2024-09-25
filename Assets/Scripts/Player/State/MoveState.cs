using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateMachine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MoveState : MovementState
{
    //private CharacterController _controller;
    private PlayerStateMachine _player;
    private PlayerData _playerData;
    private Animator _anim;
    private float _originSpeed;
    private Vector3 _moveDir;
    private Rigidbody _rigid;
    private float canMoveTime;
    private float currentTime;

    public MoveState(PlayerStateMachine player)
    {
        _player = player;
        _playerData = _player.PlayerData;
        _originSpeed = _playerData.Speed;
        //_controller = _player.GetComponent<CharacterController>();
        _rigid = _player.Rigid;
        canMoveTime = _playerData.StiffnessTime;
    }
    public override void Enter()
    {
        Debug.Log("현재 Walk State에 진입!");
        _anim = _playerData.FireStates[(int)_playerData.CurFireWeapon].GetComponent<Animator>();
    }

    public override void Update()
    {
        if (_playerData.IsDamage == true)
        {
            currentTime += Time.deltaTime;
            if(currentTime > canMoveTime)
            {
                _playerData.IsDamage = false;
                currentTime = 0;
            }
        }
        if(_playerData.IsDamage == false)
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
    }

    private void Walk()
    {
        _moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 dir = _player.transform.rotation * _moveDir * _playerData.Speed;
        dir.y = 0f;

        _rigid.velocity = dir;
        //_controller.Move(dir);

    }

    private void Run()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(_playerData.RunGage > 0)
            {
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
        _playerData.Speed = _originSpeed;
        _anim.SetBool("isRun", false);
    }
}
