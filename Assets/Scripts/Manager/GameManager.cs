using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public enum GameState { 준비, 시작, 끝}
public class GameManager : MonoBehaviour
{

    [SerializeField] private AudioClip _bgm;
    [SerializeField] private TimelineAsset _startTimeLine;
    private float _finishStartTime;
    private float _currentTime;
    private bool _isGameStart;
    public GameState GameState;



    private void Awake()
    {
        GameState = GameState.준비;
        SoundManager.Instance.PlayeBGM(_bgm);
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
        if(_isGameStart == false)
        {
            _isGameStart = true;
            GameState = GameState.시작;
        }
    }

}
