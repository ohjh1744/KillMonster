using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss1ThrowAttack : BossAttack
{
    [SerializeField] AudioSource _audioSource;

    [SerializeField] private GameObject _throwObject;

    [SerializeField] private GameObject _hitPoint;

    [SerializeField] private Transform _thhrowPos;

    [SerializeField] private Animator _anim;

    [SerializeField] private float _finishAttackTime;

    [SerializeField] private float _showHitTime;

    private WaitForSeconds _finishAttackSeconds;

    private WaitForSeconds _showHitSeconds;

    private  Coroutine _coroutine;


    public void Awake()
    {
        IsAttack = true;
        _finishAttackSeconds = new WaitForSeconds(_finishAttackTime);
        _showHitSeconds = new WaitForSeconds(_showHitTime);
    }

    public override void Attack(float bossBasicDamage)
    {
        _coroutine = StartCoroutine(ThrowAttack( bossBasicDamage));
    }

    public override void StopAttack()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }


    private IEnumerator ThrowAttack(float bossBasicDamage)
    {
        GameObject hitPoint = Instantiate(_hitPoint);
        Debug.Log(Target.position);
        Vector3 hitPointPostion = Target.position;
        hitPointPostion.y = 0.1f;
        hitPoint.transform.position = hitPointPostion;

        yield return _showHitSeconds;

        Destroy(hitPoint);
        GameObject throwObject = Instantiate(_throwObject);
        throwObject.transform.position = _thhrowPos.position;
        IThrowable throwable = throwObject.GetComponent<IThrowable>();
        throwable.Target = hitPointPostion;
        throwable.Throw(bossBasicDamage, _audioSource);

        yield return _finishAttackSeconds;

        IsAttack = false;
        _coroutine = null;
    }
    
}
