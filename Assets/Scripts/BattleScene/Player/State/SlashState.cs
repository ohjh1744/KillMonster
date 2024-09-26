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
        _knifePos = _playerData.NotFireAttackPos[(int)NotFireWeapon.Ä®].transform;
        _playerDamage = _playerData.Damage;
    }
    public override void Enter()
    {
        Debug.Log("ÇöÀç SlashState¿¡ ÁøÀÔ!");
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
        Debug.Log("ÇöÀç SlashState¿¡¼­ ³ª°¨!");
        _anim.SetBool("isSlash", false);
    }

    private void Slash()
    {
        float attackTime = _playerData.NotFireWeapons[(int)NotFireWeapon.Ä®].GetComponent<IAttackTime>().AttackTime;

        if (Time.time - _playerData.NotFireLastAttackTime[(int)NotFireWeapon.Ä®] > attackTime)
        {
            ICuttable cuttable = _playerData.NotFireWeapons[(int)NotFireWeapon.Ä®].GetComponent<ICuttable>();
            cuttable.Cut(_knifePos.position, _playerDamage);

            _playerData.NotFireLastAttackTime[(int)NotFireWeapon.Ä®] = Time.time;
            _anim.SetBool("isSlash", true);
            _anim.Play(_slashHash);
        }
    }



}
