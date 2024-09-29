using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    //SoundManager gameobject아래에 bgm, sfx object를 자식으로 둠.
    // 배경음은 거리상관없이 2d사운드식으로
    [SerializeField] private AudioSource _bgm;
    //레벨업 같은 소리는 2d사운드식으로 거리상관없이 그냥 들리도록하기위해서
    public AudioSource Sfx;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayeBGM(AudioClip clip)
    {
        _bgm.clip = clip;
        _bgm.Play();
    }

    public void StopBGM()
    {
        if (_bgm.isPlaying == false)
        {
            return;
        }
        _bgm.Stop();
    }

    public void PauseBGM()
    {
        if (_bgm.isPlaying == false)
        {
            return;
        }
        _bgm.Pause();
    }

    public void SetBGm(float volume, float pitch = 1f)
    {
        _bgm.volume = volume;
        _bgm.pitch = pitch;
    }

    public void PlaySFX(AudioClip clip)
    {
        Sfx.PlayOneShot(clip);
    }

    public void SetSFX(float volume, float pitch = 1f)
    {
        Sfx.volume = volume;
        Sfx.pitch = pitch;
    }

    public void StopSFX()
    {
        if (Sfx.isPlaying == false)
        {
            return;
        }
        Sfx.Stop();
    }

}
