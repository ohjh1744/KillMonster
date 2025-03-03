using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossThirdAttackState : BossState
{
    private BossStateMachine _boss;

    private BossData _bossData;

    private NavMeshAgent _navMesh;

    private BossAttack _bossAttack;

    private Animator _anim;

    private int _thirdAttackHash = Animator.StringToHash("ThirdAttack");

    public BossThirdAttackState(BossStateMachine boss, BossAttack bossAttack)
    {
        this._boss = boss;
        _anim = _boss.GetComponent<Animator>();
        _bossData = _boss.BossData;
        _navMesh = _boss.GetComponent<NavMeshAgent>();
        _bossAttack = bossAttack;
    }
    public override void Enter()
    {
        Debug.Log("BossThirdAttack 진입");
        _navMesh.enabled = false;
        _boss.transform.LookAt(_boss.Player.transform);
        _anim.Play(_thirdAttackHash, -1, 0);
        _bossAttack.Target = _boss.Player.transform;
        _bossAttack.IsAttack = true;
        DoAttack();
    }

    public override void Update()
    {
        Debug.Log(_bossAttack.IsAttack);
        if (_bossData.Hp < 1)
        {
            _boss.IsChange = true;
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Dead]);
        }
        else if (_bossAttack.IsAttack == false)
        {
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Move]);
        }
    }

    public override void Exit()
    {
        _bossAttack.StopAttack();
        _boss.IsChange = false;
        Debug.Log("BossThirdAttack 나감");
    }

    private void DoAttack()
    {
        switch (_bossAttack.AttackType)
        {
            case AttackType.BaseAttack:
                _bossAttack.Attack(_bossData.Damage);
                break;
            case AttackType.UsingSpeedAttack:
                _bossAttack.Attack(_bossData.Speed, _bossData.Damage);
                break;
            case AttackType.UsingAnimAttack:
                _bossAttack.Attack(_bossData.Damage, _thirdAttackHash);
                break;
        }
    }
}
