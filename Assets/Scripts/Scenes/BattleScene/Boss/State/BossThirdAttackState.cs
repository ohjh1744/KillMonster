using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossThirdAttackState : BossState
{
    private BossStateMachine _boss;

    private BossData _bossData;

    private NavMeshAgent _navMesh;

    private BossAttack _bossRushAttack;

    private Animator _anim;

    private int _thirdAttackHash = Animator.StringToHash("ThirdAttack");

    public BossThirdAttackState(BossStateMachine boss)
    {
        this._boss = boss;
        _anim = _boss.GetComponent<Animator>();
        _bossData = _boss.BossData;
        _navMesh = _boss.GetComponent<NavMeshAgent>();
        _bossRushAttack = _bossData.GetComponent<Boss1RushAttack>();
        _bossRushAttack.IsAttack = true;
    }
    public override void Enter()
    {
        Debug.Log("BossThirdAttack 진입");
        _boss.transform.LookAt(_boss.Player.transform);
        _anim.Play(_thirdAttackHash, -1, 0);
        _bossRushAttack.IsAttack = true;
        _bossRushAttack.Attack(_bossData.Speed, _bossData.Damage);
    }

    public override void Update()
    {
        Debug.Log(_bossRushAttack.IsAttack);
        if (_bossData.Hp < 1)
        {
            _boss.IsChange = true;
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Dead]);
        }
        else if (_bossRushAttack.IsAttack == false)
        {
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Move]);
        }
    }

    public override void Exit()
    {
        _bossRushAttack.StopAttack();
        _boss.IsChange = false;
        Debug.Log("BossThirdAttack 나감");
    }
}
