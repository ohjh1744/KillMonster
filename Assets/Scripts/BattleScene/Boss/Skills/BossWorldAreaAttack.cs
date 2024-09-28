using Cinemachine;
using System.Collections;
using System.Text;
using UnityEngine;


public class BossWorldAreaAttack : MonoBehaviour
{
    public bool IsAttack;
    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _attackNum;
    [SerializeField] private float _waitRoarTime;
    [SerializeField] private float _damageRateTime;
    [SerializeField] private CinemachineVirtualCamera _playerNoiseCamera;
    [SerializeField] private GameObject _safeZone;
    [SerializeField] private Transform[] _safeZonePoints;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _attackClip;
    [SerializeField] private Animator _anim;

    private int _originPriority;
    private int _safeZonePointNum;
    private WaitForSeconds _damageRateSeconds;
    private WaitForSeconds _waitRoarSeconds;
    private Coroutine _coroutine;

    void Awake()
    {
        IsAttack = true;
        _waitRoarSeconds = new WaitForSeconds(_waitRoarTime);
        _damageRateSeconds = new WaitForSeconds(_damageRateTime);
        _originPriority = _playerNoiseCamera.Priority;
    }

    public void Attack(int bossDamage, string animName)
    {
        _coroutine = StartCoroutine(RoarAttack(bossDamage, animName));
    }

    private IEnumerator RoarAttack(int bossDamage, string st)
    {
        if (_safeZone != null)
        {
            _safeZone.SetActive(true);
            _safeZonePointNum = Random.Range(0, _safeZonePoints.Length);
            _safeZone.transform.position = _safeZonePoints[_safeZonePointNum].position;
        }

        _anim.Play("Idle");
        yield return _waitRoarSeconds;

        _audioSource.spatialBlend = 0f;
        _audioSource.PlayOneShot(_attackClip);
        _anim.Play(st);

        _playerNoiseCamera.Priority = _originPriority * 2;
        int num = 0;
        while (num < _attackNum)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _range, LayerMask.GetMask("Damagable"));
            if (hits.Length > 0)
            {
                foreach (Collider hit in hits)
                {
                    if (hit.gameObject.tag == "Player")
                    {
                        PlayerData playerData = hit.GetComponent<PlayerData>();
                        if (playerData.IsSafe == true)
                        {
                            continue;
                        }
                        IDamagable damagable = hit.GetComponent<IDamagable>();
                        damagable.TakeDamage(bossDamage + _damage);
                        Rigidbody rigid = hit.GetComponent<Rigidbody>();
                    }
                }
            }

            num++;
            yield return _damageRateSeconds;
        }

        _audioSource.spatialBlend = 1f;
        _playerNoiseCamera.Priority = _originPriority;
        if(_safeZone != null)
        {
            _safeZone.SetActive(false);
        }
        IsAttack = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

}
