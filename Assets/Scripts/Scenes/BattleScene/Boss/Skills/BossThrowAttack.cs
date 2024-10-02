using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossThrowAttack : MonoBehaviour, IBossThrowAttack
{
    [SerializeField] AudioSource _audioSource;

    [SerializeField] private GameObject _throwObject;

    [SerializeField] private GameObject _hitPoint;

    [SerializeField] private Transform ThrowPos;

    [SerializeField] private Animator _anim;

    [SerializeField] private float _finishAttackTime;

    [SerializeField] private float _showHitTime;
    public Transform Target {  get;  set; }

    private WaitForSeconds _FinishAttackSeconds;

    private WaitForSeconds _showHitSeconds;

    private  Coroutine _coroutine;

    private bool _isAttack;
    public bool IsAttack { get { return _isAttack; }  set { _isAttack = value; } }

    public void Awake()
    {
        IsAttack = true;
        _FinishAttackSeconds = new WaitForSeconds(_finishAttackTime);
        _showHitSeconds = new WaitForSeconds(_showHitTime);
    }

    public void Attack(float bossBasicDamage)
    {
        _coroutine = StartCoroutine(ThrowAttack( bossBasicDamage));
    }

    public void StopAttack()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }


    private IEnumerator ThrowAttack(float bossBasicDamage)
    {
        GameObject hitPoint = Instantiate(_hitPoint);
        Vector3 hitPointPostion = Target.position;
        hitPointPostion.y = 0.1f;
        hitPoint.transform.position = hitPointPostion;

        yield return _showHitSeconds;

        Destroy(hitPoint);
        GameObject throwObject = Instantiate(_throwObject);
        throwObject.transform.position = ThrowPos.position;
        IThrowable throwable = throwObject.GetComponent<IThrowable>();
        throwable.Target = hitPointPostion;
        throwable.Throw(bossBasicDamage, _audioSource);

        yield return _FinishAttackSeconds;

        IsAttack = false;
        _coroutine = null;
    }
    
}
