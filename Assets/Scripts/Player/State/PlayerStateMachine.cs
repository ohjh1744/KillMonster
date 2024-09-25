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
    [HideInInspector] public Rigidbody Rigid;
    [HideInInspector] public PlayerData PlayerData;
    [SerializeField] private Image _bloody;
    [SerializeField] private float _turnBloodyTime;
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
        ChangeMovementState(MovementStates[(int)EMovementState.Idle]);
        ChangeAttackState(AttackStates[(int)EAttackState.IdleAttack]);
    }

    // Update is called once per frame
    private void Update()
    {
        _currentMovementState?.Update();
        _currentAttackState?.Update();
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
        PlayerData.IsDamage = true;
        PlayerData.Hp -= damage;
        _turnBloodyRoutine = StartCoroutine(TurnBloodyScreen());
    }

    IEnumerator TurnBloodyScreen()
    {
        _bloody.gameObject.SetActive(true);

        yield return _turnBloodySeconds;

        _bloody.gameObject.SetActive(false);
        _turnBloodyRoutine = null;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, 1f);
    //}

}
