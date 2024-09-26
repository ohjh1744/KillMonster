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
    private Camera _camera;

    private float _playerDamage;
    private int _fireHash = Animator.StringToHash("Fire");
    private int _zoomFireHash = Animator.StringToHash("ZoomFire");
    public FireState(PlayerStateMachine player)
    {
        _player = player;
        _playerData = _player.PlayerData;
        _camera = _player.PlayerData.Camera;
        _moveCamera = _playerData.GetComponent<MoveCamera>();
        _playerDamage = _playerData.Damage;
    }

    public override void Enter()
    {
        Debug.Log("«ˆ¿Á Fire Stateø° ¡¯¿‘!");
        _anim = _playerData.FireStates[(int)_playerData.CurFireWeapon].GetComponent<Animator>();
    }
    public override void Update()
    {
        Fire();
        if ((_playerData.IsZoom == Zoom.¡‹æ∆øÙ && Input.GetMouseButtonUp(0)))
        {
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.IdleAttack]);
        }
        if (_playerData.IsZoom == Zoom.¡‹¿Œ && Input.GetMouseButtonUp(0))
        {
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.Zoom]);
        }
    }

    public override void Exit()
    {
        Debug.Log("Fire Stateø°º≠ ≥™∞®!");
        _anim.SetBool("isFire", false);
    }
    private void Fire()
    {
        float attackTime = _playerData.FireWeapons[(int)_playerData.CurFireWeapon].GetComponent<IAttackTime>().AttackTime;

        if (_playerData.GetAmmos((int)_playerData.CurFireWeapon) > 0)
        {
            if (Time.time - _playerData.FireLastAttackTime[(int)_playerData.CurFireWeapon] > attackTime)
            {
                IShootable shootable = _playerData.FireWeapons[(int)_playerData.CurFireWeapon].GetComponent<IShootable>();
                shootable.Shoot(_camera, _playerDamage);
                _moveCamera.ApplyRecoil(shootable.ReCoil);

                _playerData.SetAmmos((int)_playerData.CurFireWeapon , _playerData.GetAmmos((int)_playerData.CurFireWeapon) - 1);

                _playerData.FireLastAttackTime[(int)_playerData.CurFireWeapon] = Time.time;
                _anim.SetBool("isFire", true);
                if(_playerData.IsZoom == Zoom.¡‹æ∆øÙ)
                {
                    _anim.Play(_fireHash, -1, 0);
                }
                else if(_playerData.IsZoom  == Zoom.¡‹¿Œ)
                {
                    _anim.Play(_zoomFireHash, -1, 0);
                }
            }
        }

    }

}
