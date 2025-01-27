using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSecondAttackState : BossState
{
    private BossStateMachine _boss;

    private BossData _bossData;

    private NavMeshAgent _navMesh;

    private BossAttack _bossHitAttack;

    private Animator _anim;

    private int _secondAttackHash = Animator.StringToHash("SecondAttack");
    public BossSecondAttackState(BossStateMachine boss)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
        _navMesh = _bossData.GetComponent<NavMeshAgent>();
        _bossHitAttack = boss.GetComponent<Boss1HitAttack>();
        _anim = _boss.GetComponent<Animator>();

    }
    public override void Enter()
    {
        Debug.Log("BossSecondAttack 진입");
        _navMesh.enabled = false;
        _boss.transform.LookAt(_boss.transform);
        _anim.Play(_secondAttackHash, -1, 0);
        _bossHitAttack.IsAttack = true;
        _bossHitAttack.Attack(_bossData.Damage);
    }

    public override void Update()
    {
        if (_bossData.Hp < 1)
        {
            _boss.IsChange = true;
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Dead]);
        }
        if (_bossHitAttack.IsAttack == false)
        {
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Move]);
        }
    }
    public override void Exit()
    {
        _bossHitAttack.StopAttack();
        _boss.IsChange = false;
        Debug.Log("BossSecondAttack 나감");
    }
}
