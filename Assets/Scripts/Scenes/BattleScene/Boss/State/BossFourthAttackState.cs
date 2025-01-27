using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossFourthAttackState : BossState
{
    private BossStateMachine _boss;

    private BossData _bossData;

    private NavMeshAgent _navMesh;

    private BossAttack _bossAttack;

    private Animator _anim;

    private Animator _warningAnim;

    private int _warningAnimTrueHash = Animator.StringToHash("WarningImageTrue");

    private int _warningAnimFalseHash = Animator.StringToHash("WarningImageFalse");

    private int _FourthAttackHash = Animator.StringToHash("FourthAttack");
    public BossFourthAttackState(BossStateMachine boss, EBossAttack bossAttack)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
        _navMesh = _boss.GetComponent<NavMeshAgent>();
        _bossAttack = _boss.SetAttack(bossAttack);
        _anim = _boss.GetComponent<Animator>();
        _warningAnim = _boss.FourthAttackWarningImage.GetComponent<Animator>();
    }
    public override void Enter()
    {
        Debug.Log("BossFourthAttack 진입");
        _navMesh.enabled = false;
        _boss.transform.LookAt(_boss.Player.transform);
        _anim.Play(_FourthAttackHash, -1, 0);
        _bossAttack.Target = _boss.Player.transform;
        _bossAttack.IsAttack = true;
        _bossAttack.Attack(_bossData.Damage, _FourthAttackHash);
        _warningAnim.Play(_warningAnimTrueHash);
    }

    public override void Update()
    {
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
        _warningAnim.Play(_warningAnimFalseHash);
        _boss.IsChange = false;
        Debug.Log("BossFourthAttack 나감");
    }
}
