using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateMachine;

public class IdleAttackState : AttackState
{
    private PlayerStateMachine _player;
    PlayerData _playerData;
    public IdleAttackState(PlayerStateMachine player)
    {
        _player = player;
        _playerData = player.PlayerData;
    }
    public override void Enter()
    {
        Debug.Log("���� Idle Attack State�� ����!");
    }

    public override void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapFireWeapon(FireWeapon.����);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapFireWeapon(FireWeapon.������);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.Fire]);
        }
        if (Input.GetMouseButtonDown(1))
        {
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.Zoom]);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.Slash]);
        }
        if(Input.GetKeyDown(KeyCode.R) || _playerData.GetAmmos((int)_playerData.CurFireWeapon) == 0)
        {
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.ReLoad]);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.Throw]);
        }
    }

    public override void Exit()
    {
        Debug.Log("Idle Attack State���� ����!");
    }

    private void SwapFireWeapon(FireWeapon afterFireWeapon)
    {
        // �������.fase
        _playerData.FireStates[(int)_playerData.CurFireWeapon].SetActive(false);
        //���º�ȭ
        _playerData.CurFireWeapon = afterFireWeapon;
        //���� true
        _playerData.FireStates[(int)_playerData.CurFireWeapon].SetActive(true);

        _playerData.SetAmmos((int)_playerData.CurFireWeapon, _playerData.GetAmmos((int)_playerData.CurFireWeapon));
    }
}
