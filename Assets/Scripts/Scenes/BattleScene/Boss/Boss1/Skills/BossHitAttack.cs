using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitAttack : MonoBehaviour, IBossHitAttack
{
    private bool _isAttack;
    public float AttackDistance;
    [SerializeField] Transform _attackPos;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _attackClip;
    [SerializeField] private GameObject _hitPoint;
    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    [SerializeField] private float _pullPower;
    [SerializeField] private float _finishAttackTime;
    [SerializeField] private float _showHitTime;
    private WaitForSeconds _FinishAttackSeconds;
    private WaitForSeconds _showHitSeconds;
    private Coroutine _coroutine;

    public bool IsAttack { get { return _isAttack; } set { _isAttack = value; } }
    void Awake()
    {
        IsAttack = true;
        _FinishAttackSeconds = new WaitForSeconds(_finishAttackTime);
        _showHitSeconds = new WaitForSeconds(_showHitTime);
    }

    public void Attack(float bossDamage)
    {
        _coroutine = StartCoroutine(HitAttack(bossDamage));
    }

    public void StopAttack()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator  HitAttack(float bossDamage)
    {
        _hitPoint.SetActive(true);
        _audioSource.clip = _attackClip;
        _audioSource.Play();

        yield return _showHitSeconds;

        _hitPoint.SetActive(false);
        Collider[] hits = Physics.OverlapSphere(_attackPos.position, _range, LayerMask.GetMask("Damagable"));
        if (hits.Length > 0)
        {
            foreach (Collider hit in hits)
            {
                if(hit.gameObject.tag == "Player")
                {
                    IDamagable damagable = hit.GetComponent<IDamagable>();
                    damagable.TakeDamage(bossDamage + _damage);
                    Rigidbody rigid = hit.GetComponent<Rigidbody>();
                    rigid.AddForce(transform.forward * _pullPower, ForceMode.Impulse);
                }
            }
        }

        yield return _FinishAttackSeconds;
        IsAttack = false;
        _coroutine = null;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPos.position, _range);
    }
}
