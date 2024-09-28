using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossRushAttack : MonoBehaviour
{
    public bool IsAttack;
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

    void Awake()
    {
        IsAttack = true;
        _damageRateSeconds = new WaitForSeconds(_damageRateTime);
        _waitRushSeconds = new WaitForSeconds(_waitRushTime);
    }

    public void Attack(float basicSpeed,int bossDamage, NavMeshAgent navMeshAgent, Animator anim, AudioClip attackClip, AudioSource audioSource)
    {
        _coroutine = StartCoroutine(RushAttack(basicSpeed, bossDamage, navMeshAgent, anim, attackClip, audioSource));
    }

    private IEnumerator RushAttack(float basicSpeed, int bossDamage, NavMeshAgent navMeshAgent, Animator anim, AudioClip attackClip, AudioSource audioSource)
    {

        anim.speed = 0;
        navMeshAgent.speed = 0;

        yield return _waitRushSeconds;

        audioSource.PlayOneShot(attackClip);
        anim.speed = 1;
        float originSpeed = navMeshAgent.speed;
        navMeshAgent.speed  = _rushSpeed + basicSpeed;
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
                        damagable.TakeDamage(bossDamage + _damage);
                        Rigidbody rigid = hit.GetComponent<Rigidbody>();
                        rigid.AddForce(transform.forward * _pullPower, ForceMode.Impulse);
                    }
                }
            }

            num++;
            yield return _damageRateSeconds;
        }

        navMeshAgent.speed = originSpeed;
        IsAttack = false;
        _coroutine = null;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
