using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EBossState {Idle, Move, Upset, Dead, FirstAttack, SecondAttack, ThirdAttack, FourthAttack, Size}
public class Boss1StateMachine : MonoBehaviour
{
    [SerializeField] private BossData _bossData;
    public BossData BossData { get { return _bossData; } private set { } }

    [SerializeField] private GameManager _gameManager;
    public GameManager GameManager { get { return _gameManager; } private set { } }

    [SerializeField] private float _changeStateTime;

    [SerializeField] private GameObject _player;
    public GameObject Player { get { return _player; } private set { } }

    [SerializeField] private Image _fourthAttackWarningImage;
    public Image FourthAttackWarningImage { get { return _fourthAttackWarningImage; } private set { } }

    [SerializeField] private Image _upsetWarningImage;
    public Image UpsetWarningImage { get { return _upsetWarningImage; } private set { } }

    private BossState _state;

    private BossState[] _bossStates = new BossState[(int)EBossState.Size];
    public BossState[] BossStates { get { return _bossStates; } private set { } }
    public int StateProbability { get; private set; }
    public bool _isChange { get; set; }
    private Coroutine _coroutine { get; set; }

    private WaitForSeconds _seconds;
    

    private void Awake()
    {
        BossStates[(int)EBossState.Idle] = new Boss1IdleState(this);
        BossStates[(int)EBossState.Move] = new Boss1MoveState(this);
        BossStates[(int)EBossState.Upset] = new Boss1UpsetState(this);
        BossStates[(int)EBossState.Dead] = new Boss1DeadState(this);
        BossStates[(int)EBossState.FirstAttack] = new Boss1FirstAttackState(this);
        BossStates[(int)EBossState.SecondAttack] = new Boss1SecondAttackState(this);
        BossStates[(int)EBossState.ThirdAttack] = new Boss1ThirdAttackState(this);
        BossStates[(int)EBossState.FourthAttack] = new Boss1FourthAttackState(this);
        _seconds = new WaitForSeconds(_changeStateTime);
    }
    private void Start()
    {
        ChangeState(BossStates[(int)EBossState.Idle]);
        _coroutine = StartCoroutine(ChangeStateProbability());
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.GameState == EGameState.Start || GameManager.GameState == EGameState.BossDead)
        {
            _state?.Update();
        }
    }

    public void ChangeState(BossState newState)
    {
        if (_state != null)
        {
            _state.Exit();
        }

        _state = newState;
        _state.Enter();
    }

    IEnumerator ChangeStateProbability()
    {
        while (true)
        {
            StateProbability = Random.Range(0, 100);
            yield return _seconds;
        }
    }




}
