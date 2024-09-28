using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossFourthAttack : BossState
{
    private BossStateMachine _boss;
    private BossData _bossData;
    private NavMeshAgent _navMesh;
    private IBossWorldAreaAttack _bossWorldAreaAttack;
    private Animator _anim;
    private Animator _warningAnim;
    private int _warningAnimTrueHash = Animator.StringToHash("WarningImageTrue");
    private int _warningAnimFalseHash = Animator.StringToHash("WarningImageFalse");
    private int _FourthAttackHash = Animator.StringToHash("FourthAttack");
    public BossFourthAttack(BossStateMachine boss)
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
        Debug.Log("BossFourthAttack ����");
        _navMesh.enabled = false;
        _boss.transform.LookAt(_boss.Player.transform);
        _anim.Play(_FourthAttackHash, -1, 0);
        _bossWorldAreaAttack.IsAttack = true;
        _bossWorldAreaAttack.Attack(_bossData.BasicDamage, _FourthAttackHash);
        _warningAnim.Play(_warningAnimTrueHash);
    }

    public override void Update()
    {
        if (_bossWorldAreaAttack.IsAttack == false)
        {
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Move]);
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        _warningAnim.Play(_warningAnimFalseHash);
        _bossWorldAreaAttack.IsAttack = true;
        Debug.Log("BossFourthAttack ����");
    }
}
