using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Explosive : MonoBehaviour
{

    [SerializeField] protected int _damage;
    [SerializeField] protected float _range;
    [SerializeField] protected GameObject _explosion;
    protected Rigidbody _rigid;
    protected Coroutine _coroutine;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }


    public IEnumerator Explode()
    {
        GameObject explosion = Instantiate(_explosion);
        explosion.transform.position = gameObject.transform.position;
        explosion.SetActive(true);
        Collider[] hits = Physics.OverlapSphere(transform.position, _range, LayerMask.GetMask("Damagable"));
        if (hits.Length > 0)
        {
            foreach (Collider hit in hits)
            {
                IDamagable damagable = hit.GetComponent<IDamagable>();
                if (hit.gameObject.tag != "Player")
                {
                    damagable.TakeDamage(_damage);
                }
            }
        }

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
        explosion.SetActive(false);
        StopCoroutine(_coroutine);
        _coroutine = null;
    }

}
