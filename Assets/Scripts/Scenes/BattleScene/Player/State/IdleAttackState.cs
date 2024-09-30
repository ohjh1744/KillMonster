using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateMachine;

public class IdleAttackState : AttackState
{
    private PlayerStateMachine _player;
    private PlayerData _playerData;
    private AudioClip _changeGunClip;
    private float _currentTime; 
    public IdleAttackState(PlayerStateMachine player)
    {
        _player = player;
        _playerData = player.PlayerData;
        _changeGunClip = _playerData.AudioClips[(int)ESound.ChangeFireWeapon];
    }
    public override void Enter()
    {
        Debug.Log("현재 Idle Attack State에 진입!");
    }

    public override void Update()
    {
        if(_playerData.IsChangeFireWeapon == true)
        {
            _currentTime -= Time.deltaTime;
            if(_currentTime - Time.deltaTime < 0)
            {
                _playerData.IsChangeFireWeapon = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwapFireWeapon(EFireWeapon.Main);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwapFireWeapon(EFireWeapon.Sub);
            }

            if(Input.GetMouseButtonDown(0))
            {
                _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.Fire]);
            }
            if(Input.GetMouseButtonDown(1))
            {
                _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.Zoom]);
            }
            if(Input.GetKeyDown(KeyCode.Z))
            {
                _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.Slash]);
            }
            if (Input.GetKeyDown(KeyCode.R) || _playerData.GetAmmos((int)_playerData.CurFireWeapon) == 0)
            {
                _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.ReLoad]);
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.Throw]);
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("Idle Attack State에서 나감!");
    }

    private void SwapFireWeapon(EFireWeapon afterFireWeapon)
    {
        SoundManager.Instance.PlaySFX(_changeGunClip);
        _playerData.IsChangeFireWeapon = true;
        // 현재상태.fase
        _playerData.FireStates[(int)_playerData.CurFireWeapon].SetActive(false);
        //상태변화
        _playerData.CurFireWeapon = afterFireWeapon;
        //상태 true
        _playerData.FireStates[(int)_playerData.CurFireWeapon].SetActive(true);

        _playerData.SetAmmos((int)_playerData.CurFireWeapon, _playerData.GetAmmos((int)_playerData.CurFireWeapon));

        _currentTime = _playerData.ChangeWeaponTime;
        
    }
}
