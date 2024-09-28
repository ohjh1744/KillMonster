using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMoveState : BossState
{
    private BossStateMachine _boss;
    private BossData _bossData;
    private BossHitAttack _bossHitAttack;
    private NavMeshAgent _navMesh;
    private Transform _player;
    private float _speed;
    private Animator _anim;
    private AudioSource _audioSource;
    private AudioClip _moveClip;

    private float _curMoveSoundTime;
    private float probability;
    private float _bossUpsetHp;
    public BossMoveState(BossStateMachine boss)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
        _bossHitAttack = _boss.GetComponent<BossHitAttack>();
        _navMesh = _bossData.NavMesh;
        _player = _bossData.Player.transform;
        _speed = _bossData.Speed;
        _anim = _bossData.Anim;
        _bossUpsetHp = _bossData.Hp / 2;
        _audioSource = boss.AudioSource;
        _moveClip = _bossData.AudioClips[(int)BossSound.Walk];
    }
    public override void Enter()
    {
        Debug.Log("BossMoveState에 진입");
        _navMesh.enabled = true;
        _navMesh.speed = _speed;
        _curMoveSoundTime = 0;
}

    public override void Update()
    {
        PlayMoveSound();
        Move();
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
        if (_bossData.IsUpset == true && (_boss.StateProbability > 60 && _boss.StateProbability <= 80) && _boss._isChange == false)
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

    private void PlayMoveSound()
    {
        if (Time.time - _curMoveSoundTime > _moveClip.length/ _moveClip.length)
        {
            _audioSource.PlayOneShot(_moveClip);
            _curMoveSoundTime = Time.time;
        }
    }

    private void Move()
    {
        _anim.Play("Walk");
        _navMesh.SetDestination(_player.position);
    }


}
