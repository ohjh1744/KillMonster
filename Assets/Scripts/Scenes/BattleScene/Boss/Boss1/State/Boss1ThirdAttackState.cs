using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1ThirdAttackState : BossState
{
    private Boss1StateMachine _boss;

    private BossData _bossData;

    private NavMeshAgent _navMesh;

    private IBossRushAttack _bossRushAttack;

    private Animator _anim;

    private int _thirdAttackHash = Animator.StringToHash("ThirdAttack");

    public Boss1ThirdAttackState(Boss1StateMachine boss)
    {
        this._boss = boss;
        _anim = _boss.GetComponent<Animator>();
        _bossData = _boss.BossData;
        _navMesh = _boss.GetComponent<NavMeshAgent>();
        _bossRushAttack = _bossData.GetComponent<IBossRushAttack>();
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
        if (_bossData.Hp < 1)
        {
            _boss._isChange = true;
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
        _boss._isChange = false;
        Debug.Log("BossThirdAttack 나감");
    }
}
