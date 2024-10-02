using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public enum EMovementState { Idle, Walk, Size };
public enum EAttackState { IdleAttack, Fire, Slash, Throw, Zoom, ReLoad, Size };
public class PlayerStateMachine : MonoBehaviour, IDamagable
{
    private MovementState _currentMovementState;

    private AttackState _currentAttackState;

    [SerializeField] private Rigidbody _rigid;
    public Rigidbody Rigid { get { return _rigid; } private set { } }

    [SerializeField] private PlayerData _playerData;
    public PlayerData PlayerData { get { return _playerData; } private set { } }

    [SerializeField] private GameManager _gameManager;


    private MovementState[] _movementStates = new MovementState[(int)EMovementState.Size];
    public MovementState[] MovementStates { get { return _movementStates; } private set { } }

    private AttackState[] _attackStates = new AttackState[(int)EAttackState.Size];
    public AttackState[] AttackStates { get { return _attackStates; } private set { } }


    [Header("UI")]

    [SerializeField] private float _turnBloodyTime;

    [SerializeField] private Image _bloody;

    private Coroutine _turnBloodyRoutine;

    private WaitForSeconds _turnBloodySeconds;

    [SerializeField] private Image _reLoadImage;
    public Image ReLoadImage { get { return _reLoadImage; } private set { } }

    [SerializeField] private GameObject[] _aims;
    public GameObject[] Aims { get { return _aims; } private set { } }

    [Header("Audio")]

    [SerializeField] private AudioSource _movementStateAudio;
    public AudioSource MovementStateAudio { get { return _movementStateAudio; } private set { } }

    [SerializeField] private AudioSource _attackStateAudio;
    public AudioSource AttackStateAudio { get { return _attackStateAudio; } private set{ } }

    [SerializeField] private AudioClip[] _audioClips;
    public AudioClip[] AudioClips { get { return _audioClips; } private set { } }

    [SerializeField] private float[] _audioTimes;
    public float[] AudioTimes { get { return _audioTimes; } private set { } }

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        PlayerData = GetComponent<PlayerData>();
        PlayerData.Hp = PlayerData.Hp + DataManager.Instance.SaveData.CurrentMaxHp;
        PlayerData.Damage = DataManager.Instance.SaveData.CurrentDamage;
        _movementStates[(int)EMovementState.Idle] = new IdleState(this);
        _movementStates[(int)EMovementState.Walk] = new MoveState(this);
        AttackStates[(int)EAttackState.IdleAttack] = new IdleAttackState(this);
        AttackStates[(int)EAttackState.Fire] = new FireState( this);
        AttackStates[(int)EAttackState.Slash] = new SlashState(this);
        AttackStates[(int)EAttackState.Throw] = new ThrowState(this);
        AttackStates[(int)EAttackState.Zoom] = new ZoomState(this);
        AttackStates[(int)EAttackState.ReLoad] = new ReLoadState(this);
    }
    private void Start()
    {
        _turnBloodySeconds = new WaitForSeconds(_turnBloodyTime);
        ChangeMovementState(MovementStates[(int)EMovementState.Idle]);
        ChangeAttackState(AttackStates[(int)EAttackState.IdleAttack]);
    }

    // Update is called once per frame
    private void Update()
    {

        if(_gameManager.GameState == EGameState.Start)
        {
            Dead();
            UpRunGage();
            _currentMovementState?.Update();
            _currentAttackState?.Update();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "SafeZone")
        {
            PlayerData.IsSafe = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SafeZone")
        {
            PlayerData.IsSafe = false;
        }
    }


    public void ChangeMovementState(MovementState newState)
    {
        if (_currentMovementState != null)
        {
            _currentMovementState.Exit();
        }

        _currentMovementState = newState;
        _currentMovementState.Enter();     
    }

    public void ChangeAttackState(AttackState newState)
    {
        if (_currentAttackState != null)
        {
            _currentAttackState.Exit();
        }

        _currentAttackState = newState;
        _currentAttackState.Enter();
    }

    public void TakeDamage(float damage)
    {
       if(_gameManager.GameState == EGameState.Start)
        {
            SoundManager.Instance.PlaySFX(AudioClips[(int)ESound.TakeDamage]);
            PlayerData.IsDamage = true;
            PlayerData.Hp -= damage;
            _turnBloodyRoutine = StartCoroutine(TurnBloodyScreen());
        }
    }

    IEnumerator TurnBloodyScreen()
    {
        _bloody.gameObject.SetActive(true);

        yield return _turnBloodySeconds;

        _bloody.gameObject.SetActive(false);
        _turnBloodyRoutine = null;
    }


    private void UpRunGage()
    {
        if (PlayerData.IsRun == false && PlayerData.RunGage <= PlayerData.RunMaxGage)
        {
            PlayerData.RunGage += Time.deltaTime;
        }
    }

    private void Dead()
    {
        if(PlayerData.Hp < 1)
        {
            PlayerData.Hp = 0f;
            _gameManager.GameState = EGameState.Lose;
        }
    }


}
