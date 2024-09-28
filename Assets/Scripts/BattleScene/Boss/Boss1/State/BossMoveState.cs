using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMoveState : BossState
{
    private BossStateMachine _boss;
    private BossData _bossData;
    private BossMove _bossMove;
    private BossHitAttack _bossHitAttack;
    private NavMeshAgent _navMesh;
    private Transform _player;
    private float _speed;
    private Animator _anim;
    private AudioSource _audioSource;
    private AudioClip _moveClip;

    private float probability;
    private float _bossUpsetHp;
    public BossMoveState(BossStateMachine boss)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
        _bossMove = _boss.GetComponent<BossMove>();
        _bossHitAttack = _boss.GetComponent<BossHitAttack>();
        _navMesh = _boss.GetComponent<NavMeshAgent>();
        _player = _boss.Player.transform;
        _anim = _boss.GetComponent<Animator>();
        _bossUpsetHp = _bossData.Hp / 2;
        _audioSource = boss.AudioSource;
 
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
        if (_bossData.IsUpset == false && _bossData.Hp < _bossUpsetHp && _boss._isChange == false)
        {
            _boss._isChange = true;
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Upset]);
        }
        if (_boss.StateProbability <= 30 && _boss._isChange == false)
        {
            _boss._isChange = true;
            _boss.ChangeState(_boss.BossStates[(int)EBossState.FirstAttack]);
        }
        if (Vector3.Distance(_player.position, _boss.transform.position) < _bossHitAttack.AttackDistance && _boss._isChange == false)
        {
            _boss._isChange = true;
            _boss.ChangeState(_boss.BossStates[(int)EBossState.SecondAttack]);
        }
        if ((_boss.StateProbability > 30 && _boss.StateProbability <= 60) && _boss._isChange == false)
        {
            _boss._isChange = true;
            _boss.ChangeState(_boss.BossStates[(int)EBossState.ThirdAttack]);
        }
        if (_bossData.IsUpset == true && (_boss.StateProbability > 60 && _boss.StateProbability <= 90) && _boss._isChange == false)
        {
            _boss._isChange = true;
            _boss.ChangeState(_boss.BossStates[(int)EBossState.FourthAttack]);
        }
    }

    public override void Exit()
    {
        Debug.Log("BossMoveState에 나감");
    }

    public override void FixedUpdate()
    {

    }


}
