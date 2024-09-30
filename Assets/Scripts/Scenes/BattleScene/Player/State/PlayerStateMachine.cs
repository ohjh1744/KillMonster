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
    private AudioClip _hitClip;
    [HideInInspector] public Rigidbody Rigid;
    [HideInInspector] public PlayerData PlayerData;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Image _bloody;
    [SerializeField] private float _turnBloodyTime;
    public AudioSource MovementStateAudio;
    public AudioSource AttackStateAudio;

    public MovementState[] MovementStates = new MovementState[(int)EMovementState.Size];
    public AttackState[] AttackStates = new AttackState[(int)EAttackState.Size];
    public Image ReLoadImage;

    private Coroutine _turnBloodyRoutine;
    private WaitForSeconds _turnBloodySeconds;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        PlayerData = GetComponent<PlayerData>();
        MovementStates[(int)EMovementState.Idle] = new IdleState(this);
        MovementStates[(int)EMovementState.Walk] = new MoveState(this);
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
        PlayerData.Hp = PlayerData.Hp + DataManager.Instance.SaveData.CurrentMaxHp;
        PlayerData.Damage = DataManager.Instance.SaveData.CurrentDamage;
        _hitClip = PlayerData.AudioClips[(int)Sound.피격];
        ChangeMovementState(MovementStates[(int)EMovementState.Idle]);
        ChangeAttackState(AttackStates[(int)EAttackState.IdleAttack]);
    }

    // Update is called once per frame
    private void Update()
    {

        if(_gameManager.GameState == GameState.시작 && _gameManager.IsGamePause == false)
        {
            Dead();
            UpRunGage();
            _currentMovementState?.Update();
            _currentAttackState?.Update();
        }
    }

    private void FixedUpdate()
    {
        _currentAttackState?.FixedUpdate();
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
       if(_gameManager.GameState == GameState.시작)
        {
            SoundManager.Instance.PlaySFX(_hitClip);
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
            _gameManager.GameState = GameState.끝;
            _gameManager.Lose();
        }
    }


}
