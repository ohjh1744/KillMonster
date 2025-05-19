using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMoveState : BossState
{

    private BossData _bossData;

    private BossMove _bossMove;

    private BossAttack _bossHitAttack;

    private NavMeshAgent _navMesh;

    private Transform _player;

    private float _speed;

    private float _bossUpsetHp;
    public BossMoveState(BossStateMachine boss) : base(boss)
    {
        _bossData = Boss.BossData;
        _bossMove = Boss.GetComponent<BossMove>();
        _bossHitAttack = Boss.GetComponent<BossHitAttack>();
        _navMesh = Boss.GetComponent<NavMeshAgent>();
        _player = Boss.Player.transform;
        _bossUpsetHp = _bossData.Hp / 2;
 
    }
    public override void Enter()
    {
        Debug.Log("BossMoveState에 진입");
        _navMesh.enabled = true;
        _speed = _bossData.Speed;
        _navMesh.speed = _speed;
     }

    public override void Update()
    {
        _bossMove.PlayMoveSound();
        _bossMove.Move();
        if (_bossData.Hp < 1)
        {
            Boss.IsChange = true;
            Boss.ChangeState(Boss.BossStates[(int)EBossState.Dead]);
        }
        if (_bossData.IsUpset == false && _bossData.Hp < _bossUpsetHp && Boss.IsChange == false)
        {
            Boss.IsChange = true;
            Boss.ChangeState(Boss.BossStates[(int)EBossState.Upset]);
        }
        if ((Boss.StateProbability < Boss.MaxProb[0] && Boss.StateProbability >= Boss.MinProb[0]) && Boss.IsChange == false)
        {
            Boss.IsChange = true;
            Boss.ChangeState(Boss.BossStates[(int)EBossState.FirstAttack]);
        }
        if ((Boss.StateProbability < Boss.MaxProb[1] && Boss.StateProbability >= Boss.MinProb[1]) && Boss.IsChange == false)
        {
            Boss.IsChange = true;
            Boss.ChangeState(Boss.BossStates[(int)EBossState.SecondAttack]);
        }
        if ((Boss.StateProbability < Boss.MaxProb[2] && Boss.StateProbability >= Boss.MinProb[2]) && Boss.IsChange == false)
        {
            Boss.IsChange = true;
            Boss.ChangeState(Boss.BossStates[(int)EBossState.ThirdAttack]);
        }
        if (_bossData.IsUpset == true && (Boss.StateProbability < Boss.MaxProb[3] && Boss.StateProbability >= Boss.MinProb[3]) && Boss.IsChange == false)
        {
            Boss.IsChange = true;
            Boss.ChangeState(Boss.BossStates[(int)EBossState.FourthAttack]);
        }
    }

    public override void Exit()
    {
        Debug.Log("BossMoveState에 나감");
    }



}
