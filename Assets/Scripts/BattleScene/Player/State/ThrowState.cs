using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using static PlayerStateMachine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ThrowState : AttackState
{
    private PlayerStateMachine _player;
    private Transform _throwPos;
    private GameObject grenade;
    private PlayerData _playerData;
    private Animator _anim;

    private int _throwHash = Animator.StringToHash("Throw");
    private float _attackLastTime;

    public ThrowState(PlayerStateMachine player)
    {
        _player = player;
        _playerData = _player.PlayerData;
        _throwPos = _playerData.NotFireAttackPos[(int)NotFireWeapon.수류탄].transform;
        _attackLastTime = 0f;
    }
    public override void Enter()
    {
        Debug.Log("현재 Throw State에 진입!");
        _anim = _playerData.FireStates[(int)_playerData.CurFireWeapon].GetComponent<Animator>();
    }

    public override void Update()
    {
        Throw();
        if (Input.GetKeyUp(KeyCode.T))
        {
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.IdleAttack]);
        }
    }

    public override void Exit()
    {
        Debug.Log("Throw State에 나감");
        _anim.SetBool("isThrow", false);
    }


    private void Throw()
    {
        float attackTime = _playerData.NotFireWeapons[(int)NotFireWeapon.수류탄].GetComponent<IAttackTime>().AttackTime;

        if (Time.time - _attackLastTime > attackTime && _playerData.NumGrenade > 0)
        {
            _anim.SetBool("isThrow", true);
            _anim.Play(_throwHash);
            grenade = GameObject.Instantiate(_playerData.NotFireWeapons[(int)NotFireWeapon.수류탄]);
            grenade.transform.position = _throwPos.position;
            grenade.transform.rotation = _throwPos.transform.rotation;
            _playerData.NumGrenade--;
            IThrowable throwable = grenade.GetComponent<IThrowable>();
            throwable.Throw(DataManager.Instance.SaveData.Damage);

            _attackLastTime = Time.time;
        }
    }

}
