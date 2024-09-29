using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public enum GameState { 준비, 시작, 끝}
public class GameManager : MonoBehaviour
{

    [SerializeField] private AudioClip _bgm;
    [SerializeField] private TimelineAsset _startTimeLine;
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private Image _winImage;
    [SerializeField] private Image _loseImage;
  
    private float _finishStartTime;
    private float _currentTime;
    private bool _isGameStart;
    private bool _isGameFinish;
    public GameState GameState;



    private void Awake()
    {
        GameState = GameState.준비;
        _finishStartTime = (float)_startTimeLine.duration;
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
        if (_isGameStart == false)
        {
            _isGameStart = true;
            GameState = GameState.시작;
            _mainUI.SetActive(true);
        }
    }


    public void Win()
    {
        Time.timeScale = 0f;
        _winImage.gameObject.SetActive(true);
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        _loseImage.gameObject.SetActive(true);
    }

}
