using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossUpsetState : BossState
{
    private BossStateMachine _boss;
    private BossData _bossData;
    private BossUpset _bossUpset;
    private NavMeshAgent _navMesh;
    private Animator anim;
    private Animator _warningAnim;
    private float _upsetTime;

    private float _currentTIme;
    private int _upsetHash = Animator.StringToHash("Upset");
    private int _warningAnimTrueHash = Animator.StringToHash("WarningImageTrue");
    private int _warningAnimFalseHash = Animator.StringToHash("WarningImageFalse");
    public BossUpsetState(BossStateMachine boss)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
        _bossUpset = _boss.GetComponent<BossUpset>();
        _navMesh = _boss.GetComponent<NavMeshAgent>();
        anim = _boss.GetComponent<Animator>();
        _upsetTime = _bossData.UpsetTime;
        _warningAnim = _boss.UpsetWarningImage.GetComponent<Animator>();
    }
    public override void Enter()
    {
        Debug.Log("BossUpsetState 진입");
        _navMesh.enabled = false;
        anim.Play(_upsetHash);
        _warningAnim.Play(_warningAnimTrueHash);
        _bossUpset.TurnUpset(_bossData);
    }

    public override void Update()
    {
        _currentTIme += Time.deltaTime;
        if (_bossData.Hp < 1)
        {
            _boss._isChange = true;
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Dead]);
        }
        else if (_currentTIme > _upsetTime)
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
        _boss._isChange = false;
        _currentTIme = 0;
        Debug.Log("BossUpsetState 나감");
    }


}
