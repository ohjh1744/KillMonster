using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossThrowAttack : MonoBehaviour
{
    public bool IsAttack;
    [SerializeField] private float _finishAttackTime;
    [SerializeField] private float _showHitTime;
    [SerializeField] private GameObject _throwObject;
    [SerializeField] private Transform ThrowPos;
    [SerializeField] private GameObject _hitPoint;
    [HideInInspector]public Transform Target;

    private WaitForSeconds _FinishAttackSeconds;
    private WaitForSeconds _showHitSeconds;
    private  Coroutine _coroutine;


    public void Awake()
    {
        IsAttack = true;
        _FinishAttackSeconds = new WaitForSeconds(_finishAttackTime);
        _showHitSeconds = new WaitForSeconds(_showHitTime);
    }

    public void Attack()
    {
        _coroutine = StartCoroutine(ThrowAttack());
    }

    private IEnumerator ThrowAttack()
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
        throwable.Throw();

        yield return _FinishAttackSeconds;

        IsAttack = false;
        _coroutine = null;
    }
    
}
