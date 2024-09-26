using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stone : MonoBehaviour, IThrowable
{
    [SerializeField]private float _damage;
    [SerializeField]private float _power;
    [SerializeField]private float _remainTime;
    [SerializeField] private Rigidbody _rigid;

    private Vector3 _targetPos;
    public Vector3 Target { get { return _targetPos; } set { _targetPos = value; } }

    public void Throw()
    {
        Vector3 direction = Target - transform.position;
        _rigid.AddForce(direction * _power, ForceMode.Impulse);
    }

    private void Update()
    {
        _remainTime -= Time.deltaTime;
        if(_remainTime < 0)
        {
            if(gameObject.activeSelf == true)
            {
                Destroy(gameObject);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
                damagable.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
 