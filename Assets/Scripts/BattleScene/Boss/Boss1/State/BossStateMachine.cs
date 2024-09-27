using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EBossState {Idle, Move, Upset, FirstAttack, SecondAttack, ThirdAttack, FourthAttack, Size}
public class BossStateMachine : MonoBehaviour
{
    [HideInInspector] public BossData BossData;
    [HideInInspector] public Rigidbody Rigid;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _changeStateTime;
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
        BossStates[(int)EBossState.Idle] = new BossIdleState(this);
        BossStates[(int)EBossState.Move] = new BossMoveState(this);
        BossStates[(int)EBossState.Upset] = new BossUpsetState(this);
        BossStates[(int)EBossState.FirstAttack] = new BossFirstAttackState(this);
        BossStates[(int)EBossState.SecondAttack] = new BossSecondAttackState(this);
        BossStates[(int)EBossState.ThirdAttack] = new BossThirdAttackState(this);
        BossStates[(int)EBossState.FourthAttack] = new BossFourthAttack(this);
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
        if (_gameManager.GameState == GameState.Ω√¿€)
        {
            _state?.Update();
        }
    }

    private void FixedUpdate()
    {
        _state?.FixedUpdate();
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
