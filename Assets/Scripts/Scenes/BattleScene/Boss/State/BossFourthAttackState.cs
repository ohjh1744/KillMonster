using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossFourthAttackState : BossState
{
    private BossData _bossData;

    private NavMeshAgent _navMesh;

    private BossAttack _bossAttack;

    private Animator _anim;

    private Animator _warningAnim;

    private int _warningAnimTrueHash = Animator.StringToHash("WarningImageTrue");

    private int _warningAnimFalseHash = Animator.StringToHash("WarningImageFalse");

    private int _FourthAttackHash = Animator.StringToHash("FourthAttack");
    public BossFourthAttackState(BossStateMachine boss, BossAttack bossAttack) : base(boss)
    {
        _bossData = Boss.BossData;
        _navMesh = Boss.GetComponent<NavMeshAgent>();
        _bossAttack = bossAttack;
        _anim = Boss.GetComponent<Animator>();
        _warningAnim = Boss.FourthAttackWarningImage.GetComponent<Animator>();
    }
    public override void Enter()
    {
        Debug.Log("BossFourthAttack 진입");
        _navMesh.enabled = false;
        Boss.transform.LookAt(Boss.Player.transform);
        _anim.Play(_FourthAttackHash, -1, 0);
        _bossAttack.Target = Boss.Player.transform;
        _bossAttack.IsAttack = true;
        DoAttack();
        _warningAnim.Play(_warningAnimTrueHash);
    }
    
    public override void Update()
    {
        if (_bossData.Hp < 1)
        {
            Boss.IsChange = true;
            Boss.ChangeState(Boss.BossStates[(int)EBossState.Dead]);
        }
        else if (_bossAttack.IsAttack == false)
        {
            Boss.ChangeState(Boss.BossStates[(int)EBossState.Move]);
        }
    }

    public override void Exit()
    {
        _bossAttack.StopAttack();
        _warningAnim.Play(_warningAnimFalseHash);
        Boss.IsChange = false;
        Debug.Log("BossFourthAttack 나감");
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
                _bossAttack.Attack(_bossData.Damage, _FourthAttackHash);
                break;
        }
    }

}
