using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    [SerializeField] private TextMeshProUGUI _playerHpText;

    [SerializeField] private TextMeshProUGUI _bulletNumText;

    [SerializeField] private TextMeshProUGUI _grenadeNumText;

    [SerializeField] private Slider _runBar;

    private StringBuilder _sb = new StringBuilder();

    private void OnEnable()
    {
        _playerData.OnHpChanged += UpdateHp;
        _playerData.OnAmmosChanged += UpdateBullet;
        _playerData.OnNumGrenadeChanged += UpdateGrenade;
        _playerData.OnRunGageChanged += UpdateRunGage;
    }

    private void OnDisable()
    {
        _playerData.OnHpChanged -= UpdateHp;
        _playerData.OnAmmosChanged -= UpdateBullet;
        _playerData.OnNumGrenadeChanged -= UpdateGrenade;
        _playerData.OnRunGageChanged -= UpdateRunGage;
    }

    private void Start()
    {
        UpdateHp();
        UpdateBullet();
        UpdateGrenade();
        UpdateRunGage();
    }
    private void UpdateHp()
    {
        _sb.Clear();
        _sb.Append(_playerData.Hp);
        _playerHpText.SetText(_sb);
    }

    private void UpdateBullet()
    {
        _sb.Clear();
        _sb.Append(_playerData.GetAmmos((int)_playerData.CurFireWeapon));
        _bulletNumText.SetText(_sb);
    }

    private void UpdateGrenade()
    {
        _sb.Clear();
        _sb.Append(_playerData.NumGrenade);
        _grenadeNumText.SetText(_sb);
    }

    private void UpdateRunGage()
    {
        _runBar.value = _playerData.RunGage / _playerData.RunMaxGage;
    }
}
