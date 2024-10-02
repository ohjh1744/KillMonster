using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EBossState {Idle, Move, Upset, Dead, FirstAttack, SecondAttack, ThirdAttack, FourthAttack, Size}
public class Boss1StateMachine : MonoBehaviour
{
    [HideInInspector] public BossData BossData;
    [HideInInspector] public Rigidbody Rigid;
    public GameManager GameManager;
    [SerializeField] private float _changeStateTime;
    [SerializeField] public GameObject Player;
    public Image FourthAttackWarningImage;
    public Image UpsetWarningImage;
    private BossState _state;
    public BossState[] BossStates = new BossState[(int)EBossState.Size];
    //[HideInInspector] 
    public int StateProbability;
    public bool _isChange;

    public Coroutine _coroutine;
    private WaitForSeconds seconds;
    

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        BossData = GetComponent<BossData>();
        BossStates[(int)EBossState.Idle] = new Boss1IdleState(this);
        BossStates[(int)EBossState.Move] = new Boss1MoveState(this);
        BossStates[(int)EBossState.Upset] = new Boss1UpsetState(this);
        BossStates[(int)EBossState.Dead] = new Boss1DeadState(this);
        BossStates[(int)EBossState.FirstAttack] = new Boss1FirstAttackState(this);
        BossStates[(int)EBossState.SecondAttack] = new Boss1SecondAttackState(this);
        BossStates[(int)EBossState.ThirdAttack] = new Boss1ThirdAttackState(this);
        BossStates[(int)EBossState.FourthAttack] = new Boss1FourthAttack(this);
        seconds = new WaitForSeconds(_changeStateTime);
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
            yield return seconds;
        }
    }




}
