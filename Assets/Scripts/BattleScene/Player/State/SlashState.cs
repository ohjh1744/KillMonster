using UnityEngine;
using static PlayerStateMachine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SlashState : AttackState
{
    private PlayerStateMachine _player;
    private Transform _knifePos;
    private PlayerData _playerData;
    private Animator _anim;

    private float _playerDamage;
    private int _slashHash = Animator.StringToHash("Slash");

    public SlashState(PlayerStateMachine player)
    {
        _player = player;
        _playerData = _player.PlayerData;
        _knifePos = _playerData.NotFireAttackPos[(int)NotFireWeapon.Į].transform;
        _playerDamage = _playerData.Damage;
    }
    public override void Enter()
    {
        Debug.Log("���� SlashState�� ����!");
        _anim = _playerData.FireStates[(int)_playerData.CurFireWeapon].GetComponent<Animator>();
    }
    public override void Update()
    {
        Slash();
        if (Input.GetKeyUp(KeyCode.Z))
        {
            _player.ChangeAttackState(_player.AttackStates[(int)EAttackState.IdleAttack]);
        }
    }

    public override void Exit()
    {
        Debug.Log("���� SlashState���� ����!");
        _anim.SetBool("isSlash", false);
    }

    private void Slash()
    {
        float attackTime = _playerData.NotFireWeapons[(int)NotFireWeapon.Į].GetComponent<IAttackTime>().AttackTime;

        if (Time.time - _playerData.NotFireLastAttackTime[(int)NotFireWeapon.Į] > attackTime)
        {
            ICuttable cuttable = _playerData.NotFireWeapons[(int)NotFireWeapon.Į].GetComponent<ICuttable>();
            cuttable.Cut(_knifePos.position, _playerDamage);

            _playerData.NotFireLastAttackTime[(int)NotFireWeapon.Į] = Time.time;
            _anim.SetBool("isSlash", true);
            _anim.Play(_slashHash);
        }
    }



}
