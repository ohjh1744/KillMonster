using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossThirdAttackState : BossState
{
    private BossStateMachine _boss;
    private BossData _bossData;
    private NavMeshAgent _navMesh;
    private IBossRushAttack _bossRushAttack;
    private Animator _anim;
    private int _ThirdAttackHash = Animator.StringToHash("ThirdAttack");

    public BossThirdAttackState(BossStateMachine boss)
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
        _anim.Play(_ThirdAttackHash, -1, 0);
        _bossRushAttack.IsAttack = true;
        _bossRushAttack.Attack(_bossData.Speed, _bossData.BasicDamage);
    }

    public override void Update()
    {
        if (_bossRushAttack.IsAttack == false)
        {
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Move]);
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        _boss._isChange = false;
        Debug.Log("BossThirdAttack 나감");
    }
}
