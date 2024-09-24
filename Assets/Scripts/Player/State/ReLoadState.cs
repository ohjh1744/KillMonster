using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ReLoadState : AttackState
{
    private PlayerStateMachine _player;
    private PlayerData _playerData;
    private Animator _anim;
    private IShootable _shootable;
    private int _reLoadHash = Animator.StringToHash("ReLoad");

    public ReLoadState(PlayerStateMachine player)
    {
        _player = player;
        _playerData = _player.PlayerData;
    }

    public override void Enter()
    {
        Debug.Log("현재 ReLoadState에 진입!");
        _anim = _playerData.FireStates[(int)_playerData.CurFireWeapon].GetComponent<Animator>();
        _shootable = _playerData.FireWeapons[(int)_playerData.CurFireWeapon].GetComponent<IShootable>();
        _shootable.IsReLoad = true;
        ReLoad();

    }
    public override void Update()
    {
        if (_shootable.IsReLoad == false)
        {
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.IdleAttack]);
        }
    }

    public override void Exit()
    {
        Debug.Log("ReLoadState에서 나감!");

        _anim.SetBool("isReLoad", false);
        _playerData.Ammos[(int)_playerData.CurFireWeapon] = _shootable.Bullet;
    }

    private void ReLoad()
    {
        _shootable.ReLoad();
        _anim.SetBool("isReLoad", true);
        _anim.Play(_reLoadHash);
    }

}
