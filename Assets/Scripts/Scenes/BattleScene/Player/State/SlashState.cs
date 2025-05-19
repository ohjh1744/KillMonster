using UnityEngine;
using static PlayerStateMachine;

public class SlashState : AttackState
{
    private PlayerData _playerData;

    private Transform _knifePos;

    private Animator _anim;

    private AudioSource _audioSource;

    private float _attackLastTime;

    private float _playerDamage;

    private int _slashHash = Animator.StringToHash("Slash");

    private int _isSlashHash = Animator.StringToHash("isSlash");

    public SlashState(PlayerStateMachine player): base(player)
    {
        _playerData = Player.PlayerData;
        _knifePos = _playerData.NotFireAttackPos[(int)ENotFireWeapon.Knife].transform;
        _playerDamage = _playerData.Damage;
        _attackLastTime = 0f;
        _audioSource = Player.AttackStateAudio;
    }
    public override void Enter()
    {
        Debug.Log("현재 SlashState에 진입!");
        _anim = _playerData.FireStates[(int)_playerData.CurFireWeapon].GetComponent<Animator>();
    }
    public override void Update()
    {
        Slash();
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Player.ChangeAttackState(Player.AttackStates[(int)EAttackState.IdleAttack]);
        }
    }

    public override void Exit()
    {
        Debug.Log("현재 SlashState에서 나감!");
        _anim.SetBool(_isSlashHash, false);
    }

    private void Slash()
    {
        float attackTime = _playerData.NotFireWeapons[(int)ENotFireWeapon.Knife].GetComponent<IAttackTime>().AttackTime;

        if (Time.time - _attackLastTime > attackTime)
        {
            ICuttable cuttable = _playerData.NotFireWeapons[(int)ENotFireWeapon.Knife].GetComponent<ICuttable>();
            cuttable.Cut(_knifePos.position, _playerDamage, _audioSource);

            _attackLastTime = Time.time;
            _anim.SetBool(_isSlashHash, true);
            _anim.Play(_slashHash);
        }
    }



}
