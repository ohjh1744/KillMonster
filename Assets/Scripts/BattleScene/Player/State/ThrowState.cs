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

    public ThrowState(PlayerStateMachine player)
    {
        _player = player;
        _playerData = _player.PlayerData;
        _throwPos = _playerData.NotFireAttackPos[(int)NotFireWeapon.¼ö·ùÅº].transform;
    }
    public override void Enter()
    {
        Debug.Log("ÇöÀç Throw State¿¡ ÁøÀÔ!");
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
        Debug.Log("Throw State¿¡ ³ª°¨");
        _anim.SetBool("isThrow", false);
    }


    private void Throw()
    {
        float attackTime = _playerData.NotFireWeapons[(int)NotFireWeapon.¼ö·ùÅº].GetComponent<IAttackTime>().AttackTime;

        if (Time.time - _playerData.NotFireLastAttackTime[(int)NotFireWeapon.¼ö·ùÅº] > attackTime && _playerData.NumGrenade > 0)
        {
            _anim.SetBool("isThrow", true);
            _anim.Play(_throwHash);
            grenade = GameObject.Instantiate(_playerData.NotFireWeapons[(int)NotFireWeapon.¼ö·ùÅº]);
            grenade.transform.position = _throwPos.position;
            grenade.transform.rotation = _throwPos.transform.rotation;
            _playerData.NumGrenade--;
            IThrowable throwable = grenade.GetComponent<IThrowable>();
            throwable.Throw(DataManager.Instance.SaveData.Damage);

            _playerData.NotFireLastAttackTime[(int)NotFireWeapon.¼ö·ùÅº] = Time.time;
        }
    }

}
