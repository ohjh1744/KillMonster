using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss1FourthAttack : BossState
{
    private Boss1StateMachine _boss;

    private BossData _bossData;

    private NavMeshAgent _navMesh;

    private IBossWorldAreaAttack _bossWorldAreaAttack;

    private Animator _anim;

    private Animator _warningAnim;

    private int _warningAnimTrueHash = Animator.StringToHash("WarningImageTrue");

    private int _warningAnimFalseHash = Animator.StringToHash("WarningImageFalse");

    private int _FourthAttackHash = Animator.StringToHash("FourthAttack");
    public Boss1FourthAttack(Boss1StateMachine boss)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
        _navMesh = _boss.GetComponent<NavMeshAgent>(); 
        _bossWorldAreaAttack = _boss.GetComponent<IBossWorldAreaAttack>();
        _anim = _boss.GetComponent<Animator>();
        _warningAnim = _boss.FourthAttackWarningImage.GetComponent<Animator>();
    }
    public override void Enter()
    {
        Debug.Log("BossFourthAttack 진입");
        _navMesh.enabled = false;
        _boss.transform.LookAt(_boss.Player.transform);
        _anim.Play(_FourthAttackHash, -1, 0);
        _bossWorldAreaAttack.IsAttack = true;
        _bossWorldAreaAttack.Attack(_bossData.Damage, _FourthAttackHash);
        _warningAnim.Play(_warningAnimTrueHash);
    }

    public override void Update()
    {
        if (_bossData.Hp < 1)
        {
            _boss._isChange = true;
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Dead]);
        }
        else if (_bossWorldAreaAttack.IsAttack == false)
        {
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Move]);
        }
    }

    public override void Exit()
    {
        _bossWorldAreaAttack.StopAttack();
        _warningAnim.Play(_warningAnimFalseHash);
        _boss._isChange = false;
        Debug.Log("BossFourthAttack 나감");
    }
}
