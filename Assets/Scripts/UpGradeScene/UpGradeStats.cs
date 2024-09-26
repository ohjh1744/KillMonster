using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Stat
{
    public float UpGradeStat;
    public int  UpGradeStatGold;
    public int Level;
    public int MaxLevel;
}

[System.Serializable]
public class StatUI
{
    public TextMeshProUGUI UpGradeStatText;
    public TextMeshProUGUI UpGradeStatGoldText;
    public TextMeshProUGUI LevelText;
}

public class UpGradeStats : MonoBehaviour
{
    [SerializeField] private SaveData _saveData;
    [SerializeField] private StatUI _damageUi;
    [SerializeField] private StatUI _maxHpUi;
    [SerializeField] private TextMeshProUGUI _goldText;
    private StringBuilder _sb = new StringBuilder();

    private void OnEnable()
    {
        _saveData.OnUpGradeDamageChanged += UpDateUpGradeDamage;
        _saveData.OnUpGradeMaxHpChagned += UpdateUpGradeMaxHp;
        _saveData.OnGoldChanged += UpdateGold;
        _saveData.OnUpGradeDamageGoldChanged += UpdateUpGradeDamageGold;
        _saveData.OnUpGradeMaxHpGoldChanged += UpdateUpGradeMaxHpGold;
        _saveData.OnUpGradeDmaageLevel += UpdateDamageLevel;
        _saveData.OnUpGradeMaxHpLevel += UpdateMaxHpLevel;
    }

    private void OnDisable()
    {
        _saveData.OnUpGradeDamageChanged -= UpDateUpGradeDamage;
        _saveData.OnUpGradeMaxHpChagned -= UpdateUpGradeMaxHp;
        _saveData.OnGoldChanged -= UpdateGold;
        _saveData.OnUpGradeDamageGoldChanged -= UpdateUpGradeDamageGold;
        _saveData.OnUpGradeMaxHpGoldChanged -= UpdateUpGradeMaxHpGold;
        _saveData.OnUpGradeDmaageLevel -= UpdateDamageLevel;
        _saveData.OnUpGradeMaxHpLevel -= UpdateMaxHpLevel;
    }

    private void Start()
    {
        UpDateUpGradeDamage();
        UpdateUpGradeMaxHp();
        UpdateUpGradeDamageGold();
        UpdateUpGradeMaxHpGold();
        UpdateGold();
        UpdateDamageLevel();
        UpdateMaxHpLevel();
    }

    public void UpGradeDamage()
    {
        if (_saveData.UpGradeDamageLevel < _saveData.DamageMaxLevel && _saveData.Gold >= _saveData.UpGradeDamageGold)
        {
            _saveData.Damage += _saveData.Damage;
            _saveData.Gold -= _saveData.UpGradeDamageGold;
            _saveData.UpGradeDamageGold += _saveData.UpGradeDamageGold;
            _saveData.UpGradeDamageLevel += 1;

        }
    }

    public void UpGradeMaxHp()
    {
        if (_saveData.UpGradeMaxHpLevel < _saveData.MaxHpMaxLevel && _saveData.Gold >= _saveData.UpGradeMaxHpGold)
        {
            _saveData.MaxHp += _saveData.MaxHp;
            _saveData.Gold -= _saveData.UpGradeMaxHpGold;
            _saveData.UpGradeMaxHpGold += _saveData.UpGradeMaxHpGold;
            _saveData.UpGradeMaxHpLevel += 1;
        }
    }


    private void UpDateUpGradeDamage()
    {
        _sb.Clear();
        _sb.Append(_saveData.Damage);
        _damageUi.UpGradeStatText.SetText(_sb);
    }

    private void UpdateUpGradeMaxHp()
    {
        _sb.Clear();
        _sb.Append(_saveData.MaxHp);
        _maxHpUi.UpGradeStatText.SetText(_sb);
    }

    private void UpdateUpGradeDamageGold()
    {
        _sb.Clear();
        _sb.Append(_saveData.UpGradeDamageGold);
        _damageUi.UpGradeStatGoldText.SetText(_sb);
    }

    private void UpdateUpGradeMaxHpGold()
    {
        _sb.Clear();
        _sb.Append(_saveData.UpGradeMaxHpGold);
        _maxHpUi.UpGradeStatGoldText.SetText(_sb);
    }

    private void UpdateGold()
    {
        _sb.Clear();
        _sb.Append(_saveData.Gold);
        _goldText.SetText(_sb);
    }

    private void UpdateDamageLevel()
    {
        _sb.Clear();
        _sb.Append(_saveData.UpGradeDamageLevel);
        _damageUi.LevelText.SetText(_sb);
    }

    private void UpdateMaxHpLevel()
    {
        _sb.Clear();
        _sb.Append(_saveData.UpGradeMaxHpLevel);
        _maxHpUi.LevelText.SetText(_sb);
    }



}
