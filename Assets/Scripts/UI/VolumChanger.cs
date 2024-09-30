using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumChanger : MonoBehaviour
{
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _bossSoundSlider;
    [SerializeField] private Slider _playerSoundSlider;

    [SerializeField] private AudioSource _bossAudioSource;
    [SerializeField] private AudioSource _playerAudioSource1;
    [SerializeField] private AudioSource _playerAudioSource2;

    private void Awake()
    {
        _bgmSlider?.onValueChanged.AddListener(ChangeBgmVolume);
        _sfxSlider?.onValueChanged.AddListener(ChangeSfxVolume);
        _bossSoundSlider?.onValueChanged.AddListener(ChangeBossVolume);
        _playerSoundSlider?.onValueChanged.AddListener(ChangePlayerVolume);
    }

    private void ChangeBgmVolume(float volume)
    {
        SoundManager.Instance.SetBGm(volume);
    }

    private void ChangeSfxVolume(float volume)
    {
        SoundManager.Instance.SetSFX(volume);
    }

    private void ChangeBossVolume(float volume)
    {
        _bossAudioSource.volume = volume;
    }
    private void ChangePlayerVolume(float volume)
    {
        _playerAudioSource1.volume = volume;
        _playerAudioSource2.volume = volume;
    }
}
