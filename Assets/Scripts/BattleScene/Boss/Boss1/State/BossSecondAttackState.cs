using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSecondAttackState : BossState
{
    private BossStateMachine _boss;
    private BossData _bossData;
    private NavMeshAgent _navMesh;
    private BossHitAttack _bossHitAttack;
    private Animator anim;

    public BossSecondAttackState(BossStateMachine boss)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
        _navMesh = _bossData.NavMesh;
        _bossHitAttack = _bossData.BossHitAttack;
        anim = _bossData.Anim;
    }
    public override void Enter()
    {
        Debug.Log("BossSecondAttack 진입");
        _navMesh.enabled = false;
        _boss.transform.LookAt(_bossData.Player.transform);
        anim.Play("SecondAttack", -1, 0);
        _bossHitAttack.Attack(_bossData.BasicDamage);
    }

    public override void Update()
    {
        if (_bossHitAttack.IsAttack == false)
        {
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Move]);
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        _bossHitAttack.IsAttack = true;
        _boss._isChange = false;
        Debug.Log("BossSecondAttack 나감");
    }
}
