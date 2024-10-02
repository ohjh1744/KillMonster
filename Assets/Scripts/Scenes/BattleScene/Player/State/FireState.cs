using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using static PlayerStateMachine;

public class FireState : AttackState
{

    private PlayerStateMachine _player;

    private PlayerData _playerData;

    private Animator _anim;

    private MoveCamera _moveCamera;

    private float _attackLastTime;

    private float _playerDamage;

    private int _fireHash = Animator.StringToHash("Fire");

    private int _zoomFireHash = Animator.StringToHash("ZoomFire");

    public FireState(PlayerStateMachine player)
    {
        _player = player;
        _playerData = _player.PlayerData;
        _moveCamera = _player.GetComponent<MoveCamera>();
        _playerDamage = _playerData.Damage;
        _attackLastTime = 0f;
    }

    public override void Enter()
    {
        Debug.Log("현재 Fire State에 진입!");
        _anim = _playerData.FireStates[(int)_playerData.CurFireWeapon].GetComponent<Animator>();
    }
    public override void Update()
    {
        Fire();
        if ((_playerData.IsZoom == EZoom.ZoomOut && Input.GetMouseButtonUp(0)))
        {
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.IdleAttack]);
        }
        if (_playerData.IsZoom == EZoom.ZoomIn && Input.GetMouseButtonUp(0))
        {
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.Zoom]);
        }
    }

    public override void Exit()
    {
        Debug.Log("Fire State에서 나감!");
        _anim.SetBool("isFire", false);
    }
    private void Fire()
    {
        float attackTime = _playerData.FireWeapons[(int)_playerData.CurFireWeapon].GetComponent<IAttackTime>().AttackTime;

        if (_playerData.GetAmmos((int)_playerData.CurFireWeapon) > 0)
        {
            if (Time.time - _attackLastTime > attackTime)
            {
                IShootable shootable = _playerData.FireWeapons[(int)_playerData.CurFireWeapon].GetComponent<IShootable>();
                shootable.Shoot(_playerDamage);
                _moveCamera.ApplyRecoil(shootable.ReCoil);

                _playerData.SetAmmos((int)_playerData.CurFireWeapon , _playerData.GetAmmos((int)_playerData.CurFireWeapon) - 1);

                _attackLastTime = Time.time;
                _anim.SetBool("isFire", true);
                if(_playerData.IsZoom == EZoom.ZoomOut)
                {
                    _anim.Play(_fireHash, -1, 0);
                }
                else if(_playerData.IsZoom  == EZoom.ZoomIn)
                {
                    _anim.Play(_zoomFireHash, -1, 0);
                }
            }
        }

    }

}
