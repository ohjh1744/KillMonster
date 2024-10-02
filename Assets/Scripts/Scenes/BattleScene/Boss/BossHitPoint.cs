using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHitPoint : MonoBehaviour, IDamagable
{
    [SerializeField] private BossData _bossData;

    [SerializeField] private Slider _bossHpBar;

    private void OnEnable()
    {
        _bossData.OnHpChanged += UpdateHp;
    }

    private void OnDisable()
    {
        _bossData.OnHpChanged -= UpdateHp;
    }

    private void Start()
    {
        _bossHpBar.value = _bossData.Hp / _bossData.MaxHp;
    }
    public void TakeDamage(float damage)
    {
        _bossData.Hp -= damage;
    }

    private void UpdateHp()
    {
        _bossHpBar.value = _bossData.Hp / _bossData.MaxHp;
    }
}
