using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossUpsetState : BossState
{
    private BossData _bossData;

    private BossUpset _bossUpset;

    private NavMeshAgent _navMesh;

    private Animator _anim;

    private Animator _warningAnim;

    private float _upsetTime;

    private float _currentTime;

    private int _upsetHash = Animator.StringToHash("Upset");

    private int _warningAnimTrueHash = Animator.StringToHash("WarningImageTrue");

    private int _warningAnimFalseHash = Animator.StringToHash("WarningImageFalse");

    public BossUpsetState(BossStateMachine boss) : base(boss)
    {
        _bossData = Boss.BossData;
        _bossUpset = Boss.GetComponent<BossUpset>();
        _navMesh = Boss.GetComponent<NavMeshAgent>();
        _anim = Boss.GetComponent<Animator>();
        _upsetTime = _bossData.UpsetTime;
        _warningAnim = Boss.UpsetWarningImage.GetComponent<Animator>();
    }
    public override void Enter()
    {
        Debug.Log("BossUpsetState 진입");
        _navMesh.enabled = false;
        _anim.Play(_upsetHash);
        _warningAnim.Play(_warningAnimTrueHash);
        _bossUpset.TurnUpset(_bossData);
    }

    public override void Update()
    {
        _currentTime += Time.deltaTime;
        if (_bossData.Hp < 1)
        {
            Boss.IsChange = true;
            Boss.ChangeState(Boss.BossStates[(int)EBossState.Dead]);
        }
        else if (_currentTime > _upsetTime)
        {
            Boss.ChangeState(Boss.BossStates[(int)EBossState.Move]);
        }
    }

    public override void Exit()
    {
        _warningAnim.Play(_warningAnimFalseHash);
        Boss.IsChange = false;
        _currentTime = 0;
        Debug.Log("BossUpsetState 나감");
    }


}
