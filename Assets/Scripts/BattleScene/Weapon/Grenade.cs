using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Grenade : Explosive, IAttackTime, IThrowable
{

    [SerializeField]
    private float _attackTime;
    [SerializeField]
    private float _power;
    private float _playerDamage;
    public float AttackTime { get { return _attackTime; } set { _attackTime = value; } }

    public Vector3 Target { get; set; }
    public void Throw(float damage)
    {
        _rigid.AddForce(_rigid.transform.forward * _power, ForceMode.Impulse);
        _playerDamage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            // explosion�� ���������� �߻��ϴ� ���� fix�� ����
            if(_coroutine == null)
            {
                _coroutine = StartCoroutine(Explode(_playerDamage));
            }
        }
    }
}
