using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossThirdAttackState : BossState
{
    private BossStateMachine _boss;
    private BossData _bossData;
    private NavMeshAgent _navMesh;
    private BossRushAttack _bossRushAttack;
    private Animator anim;
    private AudioClip _thirdAttackClip;
    private AudioSource _audioSource;

    public BossThirdAttackState(BossStateMachine boss)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
        _navMesh = _bossData.NavMesh;
        _bossRushAttack = _bossData.BossRushAttack;
        anim = _bossData.Anim;
        _thirdAttackClip = _bossData.AudioClips[(int)BossSound.ThirdAttack];
        _audioSource = _boss.AudioSource;
    }
    public override void Enter()
    {
        Debug.Log("BossThirdAttack 진입");
        _boss.transform.LookAt(_bossData.Player.transform);
        anim.Play("ThirdAttack", -1, 0);
        _bossRushAttack.Attack(_bossData.Speed, _bossData.BasicDamage, _navMesh, anim , _thirdAttackClip , _audioSource);
    }

    public override void Update()
    {

        if (_bossRushAttack.IsAttack == false)
        {
            _boss.ChangeState(_boss.BossStates[(int)EBossState.Move]);
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        _bossRushAttack.IsAttack = true;
        _boss._isChange = false;
        Debug.Log("BossThirdAttack 나감");
    }
}
