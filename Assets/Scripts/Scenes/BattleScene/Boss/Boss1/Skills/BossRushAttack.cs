using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossRushAttack : MonoBehaviour, IBossRushAttack
{
    private bool _isAttack;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _attackClip;
    [SerializeField] private NavMeshAgent _navMesh;
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private float _rushSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private float _pullPower;
    [SerializeField] private float _range;
    [SerializeField] private float _attackNum;
    [SerializeField] private float _waitRushTime;
    [SerializeField] private float _damageRateTime;

    private WaitForSeconds _damageRateSeconds;
    private WaitForSeconds _waitRushSeconds;
    private Coroutine _coroutine;

    public bool IsAttack { get { return _isAttack; } set { _isAttack = value; } }
    void Awake()
    {
        IsAttack = false;
        _damageRateSeconds = new WaitForSeconds(_damageRateTime);
        _waitRushSeconds = new WaitForSeconds(_waitRushTime);
    }


    private void Update()
    {
        if (IsAttack)
        {
            _navMesh.SetDestination(_playerPosition.position);
        }
    }
    public void Attack(float basicSpeed,int basicDamage)
    {
        _coroutine = StartCoroutine(RushAttack(basicSpeed, basicDamage));
    }

    private IEnumerator RushAttack(float basicSpeed, int basicDamage)
    {
        _anim.speed = 0;
        _navMesh.speed = 0;

        yield return _waitRushSeconds;

        _audioSource.clip = _attackClip;
        _audioSource.Play();
        _anim.speed = 1;
        float originSpeed = _navMesh.speed;
        _navMesh.speed  = _rushSpeed + basicSpeed;
        int num = 0;
        while ( num < _attackNum)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _range, LayerMask.GetMask("Damagable"));
            if (hits.Length > 0)
            {
                foreach (Collider hit in hits)
                {
                    if (hit.gameObject.tag == "Player")
                    {
                        IDamagable damagable = hit.GetComponent<IDamagable>();
                        damagable.TakeDamage(basicDamage + _damage);
                        Rigidbody rigid = hit.GetComponent<Rigidbody>();
                        rigid.AddForce(transform.forward * _pullPower, ForceMode.Impulse);
                    }
                }
            }

            num++;
            yield return _damageRateSeconds;
        }

        _navMesh.speed = originSpeed;
        IsAttack = false;
        _coroutine = null;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
