using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossFourthAttack : BossState
{
    private BossStateMachine _boss;
    private BossData _bossData;
    private NavMeshAgent _navMesh;
    private BossWorldAreaAttack _bossWorldAreaAttack;
    private Animator anim;
    private AudioClip _fourthAttack;
    private AudioSource _audioSource;
    public BossFourthAttack(BossStateMachine boss)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
        _navMesh = _bossData.NavMesh;
        _bossWorldAreaAttack = _boss.GetComponent<BossWorldAreaAttack>();
        anim = _bossData.Anim;
        _fourthAttack = _bossData.AudioClips[(int)BossSound.FourthAttack];
        _audioSource = _boss.AudioSource;
    }
    public override void Enter()
    {
        Debug.Log("BossFourthAttack 진입");
        _navMesh.enabled = false;
        _boss.transform.LookAt(_bossData.Player.transform);
        anim.Play("FourthAttack", -1, 0);
        _bossWorldAreaAttack.Attack(_bossData.BasicDamage, anim, "FourthAttack", _fourthAttack, _audioSource);
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
        _bossWorldAreaAttack.IsAttack = true;
        _boss._isChange = false;
        Debug.Log("BossFourthAttack 나감");
    }
}
