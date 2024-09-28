using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossFirstAttackState : BossState
{
    private BossStateMachine _boss;
    private BossData _bossData;
    private NavMeshAgent _navMesh;
    private BossThrowAttack _bossThrowAttack;
    private Animator anim;
    private AudioSource _audioSource;
    public BossFirstAttackState(BossStateMachine boss)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
        _navMesh = _bossData.NavMesh;
        _bossThrowAttack = boss.GetComponent<BossThrowAttack>();
        anim = _bossData.Anim;
        _audioSource = _boss.AudioSource;
    }
    public override void Enter()
    {
        Debug.Log("BossFirstAttack 진입");
        _navMesh.enabled = false;
        _boss.transform.LookAt(_bossData.Player.transform);
        anim.Play("FirstAttack", -1, 0);
        _bossThrowAttack.Target = _bossData.Player.transform;
        _bossThrowAttack.Attack(_bossData.BasicDamage, _audioSource);
    }

    public override void Update()
    {
        if (_bossThrowAttack.IsAttack == false)
        {
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Move]);
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        _bossThrowAttack.IsAttack = true;
        _boss._isChange = false;
        Debug.Log("BossFirstAttack 나감");
    }
}
