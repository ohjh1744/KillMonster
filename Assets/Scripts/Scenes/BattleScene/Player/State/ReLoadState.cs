using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ReLoadState : AttackState
{
    private PlayerData _playerData;

    private IShootable _shootable;

    private Animator _anim;

    private int _isReLoadhash = Animator.StringToHash("isReLoad");

    private int _reLoadHash = Animator.StringToHash("ReLoad");

    public ReLoadState(PlayerStateMachine player): base(player)
    {
        _playerData = Player.PlayerData;
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
            Player.ChangeAttackState(Player.AttackStates[(int)EAttackState.IdleAttack]);
        }
    }

    public override void Exit()
    {
        Debug.Log("ReLoadState에서 나감!");

        _anim.SetBool(_isReLoadhash, false);
        _playerData.SetAmmos((int)_playerData.CurFireWeapon, _shootable.Bullet);
    }

    private void ReLoad()
    {
        _shootable.ReLoad(Player.ReLoadImage);
        _anim.SetBool(_isReLoadhash, true);
        _anim.Play(_reLoadHash);
    }

}
