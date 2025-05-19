using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSecondAttackState : BossState
{

    private BossData _bossData;

    private NavMeshAgent _navMesh;

    private BossAttack _bossAttack;

    private Animator _anim;

    private int _secondAttackHash = Animator.StringToHash("SecondAttack");
    public BossSecondAttackState(BossStateMachine boss, BossAttack bossAttack) : base(boss)
    {
        _bossData = Boss.BossData;
        _navMesh = _bossData.GetComponent<NavMeshAgent>();
        _bossAttack = bossAttack;
        _anim = Boss.GetComponent<Animator>();
    }

    public override void Enter()
    {
        Debug.Log("BossSecondAttack 진입");
        _navMesh.enabled = false;
        Boss.transform.LookAt(Boss.transform);
        _anim.Play(_secondAttackHash, -1, 0);
        _bossAttack.Target = Boss.Player.transform;
        _bossAttack.IsAttack = true;
        DoAttack();
    }

    public override void Update()
    {
        if (_bossData.Hp < 1)
        {
            Boss.IsChange = true;
            Boss.ChangeState(Boss.BossStates[(int)EBossState.Dead]);
        }
        if (_bossAttack.IsAttack == false)
        {
            Boss.ChangeState(Boss.BossStates[(int)EBossState.Move]);
        }
    }
    public override void Exit()
    {
        _bossAttack.StopAttack();
        Boss.IsChange = false;
        Debug.Log("BossSecondAttack 나감");
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
                _bossAttack.Attack(_bossData.Damage, _secondAttackHash);
                break;
        }
    }
}
