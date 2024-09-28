using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossUpsetState : BossState
{
    private BossStateMachine _boss;
    private BossData _bossData;
    private NavMeshAgent _navMesh;
    private Animator anim;
    private AudioClip _upsetClip;
    private AudioSource _audioSource;
    private float _upsetTime;

    private float _currentTIme;
    public BossUpsetState(BossStateMachine boss)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
        _navMesh = _bossData.NavMesh;
        anim = _bossData.Anim;
        _upsetTime = _bossData.UpsetTime;
        _upsetClip = _bossData.AudioClips[(int)BossSound.Upset];
        _audioSource = _boss.AudioSource;
    }
    public override void Enter()
    {
        Debug.Log("BossUpsetState 진입");
        _navMesh.enabled = false;
        anim.Play("Upset");
        TurnUpset();
    }

    public override void Update()
    {
        _currentTIme += Time.deltaTime;
        if (_currentTIme > _upsetTime)
        {
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Move]);
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        _boss._isChange = false;
        _currentTIme = 0;
        Debug.Log("BossUpsetState 나감");
    }

    private void TurnUpset()
    {
        _audioSource.PlayOneShot(_upsetClip);
        _bossData.IsUpset = true;
        _bossData.BasicDamage *= 2;
        _bossData.Speed *= 2;
    }
}
