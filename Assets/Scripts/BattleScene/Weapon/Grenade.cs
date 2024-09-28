using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Grenade : Explosive, IAttackTime, IThrowable
{
    [SerializeField] private AudioClip _throwClip;
    [SerializeField] private AudioClip _explodeClip;
    [SerializeField] private float _attackTime;
    [SerializeField] private float _power;
    private float _playerDamage;
    public float AttackTime { get { return _attackTime; } set { _attackTime = value; } }

    public Vector3 Target { get; set; }
    public void Throw(float damage, AudioSource audioSource)
    {
        audioSource.PlayOneShot(_throwClip);
        _rigid.AddForce(_rigid.transform.forward * _power, ForceMode.Impulse);
        _playerDamage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            // explosion이 여러곳에서 발생하는 버그 fix를 위해
            if(_coroutine == null)
            {
                SoundManager.Instance.PlaySFX(_explodeClip);
                _coroutine = StartCoroutine(Explode(_playerDamage));
            }
        }
    }
}
