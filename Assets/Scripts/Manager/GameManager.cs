using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public enum GameState { �غ�, ����, ��}
public class GameManager : MonoBehaviour
{

    [SerializeField] private AudioClip _bgm;
    [SerializeField] private TimelineAsset _startTimeLine;
    [SerializeField] private Image WinImage;
    [SerializeField] private Image LoseImage;
  
    private float _finishStartTime;
    private float _currentTime;
    private bool _isGameStart;
    public GameState GameState;



    private void Awake()
    {
        GameState = GameState.�غ�;
        _finishStartTime = (float)_startTimeLine.duration;
    }

    private void Start()
    {
        SoundManager.Instance.PlayeBGM(_bgm);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _finishStartTime)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        if(_isGameStart == false)
        {
            _isGameStart = true;
            GameState = GameState.����;
        }
    }

    private void FinishGame()
    {
        Time.timeScale = 0f;
        GameState = GameState.��;
    }

    public void Win()
    {
        FinishGame();
        WinImage.gameObject.SetActive(true);
    }

    public void Lose()
    {
        FinishGame();
        LoseImage.gameObject.SetActive(true);
    }

}
