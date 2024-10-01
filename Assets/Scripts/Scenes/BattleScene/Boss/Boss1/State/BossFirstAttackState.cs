using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossFirstAttackState : BossState
{
    private BossStateMachine _boss;
    private BossData _bossData;
    private NavMeshAgent _navMesh;
    private IBossThrowAttack _bossThrowAttack;
    private Animator _anim;
    private int _firstAttackHash = Animator.StringToHash("FirstAttack");
    public BossFirstAttackState(BossStateMachine boss)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
        _navMesh = _boss.GetComponent<NavMeshAgent>();
        _bossThrowAttack = boss.GetComponent<IBossThrowAttack>();
        _anim = _boss.GetComponent<Animator>();
    }
    public override void Enter()
    {
        Debug.Log("BossFirstAttack 진입");
        _navMesh.enabled = false;
        _boss.transform.LookAt(_boss.Player.transform);
        _anim.Play(_firstAttackHash, -1, 0);
        _bossThrowAttack.Target = _boss.Player.transform;
        _bossThrowAttack.IsAttack = true;
        _bossThrowAttack.Attack(_bossData.Damage);
    }

    public override void Update()
    {
        if (_bossData.Hp < 1)
        {
            _boss._isChange = true;
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Dead]);
        }
        else if (_bossThrowAttack.IsAttack == false)
        {
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Move]);
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        _bossThrowAttack.StopAttack();
        _boss._isChange = false;
        Debug.Log("BossFirstAttack 나감");
    }
}
