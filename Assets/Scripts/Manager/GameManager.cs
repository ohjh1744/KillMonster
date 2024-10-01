using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public enum GameState { �غ�, ����, �ߴ�, �������, �¸�, �й�}
public class GameManager : MonoBehaviour
{

    [SerializeField] private AudioClip _bgm;

    [SerializeField] private TimelineAsset _startTimeLine;

    [SerializeField] private GameObject _mainUI;

    [SerializeField] private GameObject _subUI;

    [SerializeField] private Image _winImage;

    [SerializeField] private Image _loseImage;

    private float _finishStartTime;

    private float _currentTime;

    private bool _isGameStart;

    private bool _isGameFinish;

    public GameState GameState;

    private void Awake()
    {
        GameState = GameState.�غ�;
        _finishStartTime = (float)_startTimeLine.duration;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _finishStartTime)
        {
            StartGame();
        }
        PauseGmae();
        BossDead();
        Win();
        Lose();
    }

    private void StartGame()
    {
        if (_isGameStart == false)
        {
            _isGameStart = true;
            GameState = GameState.����;
            _mainUI.SetActive(true);
        }
    }

    private void PauseGmae()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (GameState == GameState.���� || GameState == GameState.�ߴ�))
        {
            if (_subUI.activeSelf == true)
            {
                GameState = GameState.����;
                Time.timeScale = 1f;
                _subUI.SetActive(false);
            }
            else
            {
                GameState = GameState.�ߴ�;
                Time.timeScale = 0f;
                _subUI.SetActive(true);
            }
        }
    }

    private void BossDead()
    {
        if(GameState == GameState.�������)
        {
            _mainUI.SetActive(false);
        }
    }

    public void Win()
    {
        if (GameState == GameState.�¸� && _isGameFinish == false)
        {
            _isGameFinish = true;
            Time.timeScale = 0f;
            _winImage.gameObject.SetActive(true);
        }
    }

    public void Lose()
    {
        if (GameState == GameState.�й� && _isGameFinish == false)
        {
            _isGameFinish = true;
            Time.timeScale = 0f;
            _loseImage.gameObject.SetActive(true);
        }
    }

}
