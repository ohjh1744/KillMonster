using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1DeadState : BossState
{
    private Boss1StateMachine _boss;

    private BossData _bossData;

    private BossDead _bossDead;

    private NavMeshAgent _navMesh;

    private GameManager _gameManager;

    private Animator _anim;

    private float _deadTime;

    private float _currentTime;

    private int _deadHash = Animator.StringToHash("Dead");

    public Boss1DeadState(Boss1StateMachine boss)
    {
        this._boss = boss;
        _bossData = _boss.BossData;
        _bossDead = _boss.GetComponent<BossDead>();
        _navMesh = _boss.GetComponent<NavMeshAgent>();
        _gameManager = _boss.GameManager;
        _deadTime = _bossData.DeadTime;
        _anim = _boss.GetComponent<Animator>();
    }
    public override void Enter()
    {
        _navMesh.enabled = false;
        _gameManager.GameState = EGameState.BossDead;
        _bossDead.Dead();
        Time.timeScale = 0.5f;
        _anim.Play(_deadHash);
        DataManager.Instance.SaveData.Gold += _bossData.Gold;
        DataManager.Instance.SaveData.GameData.ClearMissions[(int)EMission.DemonKing] = true;
        Debug.Log("BossDeadState µé¾î¿È");
    }

    public override void Update()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime > _deadTime)
        {
            _gameManager.GameState = EGameState.Win;
        }
    }

    public override void Exit()
    {

    }

}
