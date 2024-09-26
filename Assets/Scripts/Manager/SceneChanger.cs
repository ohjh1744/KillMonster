using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance { get; private set; }

    [SerializeField] private Image _loadingImage;
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private Image _loadingBG;
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private float _loadingTime;

    private Coroutine _loadingRoutine;

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
    public void ChangeScene(string sceneName)
    {
        _mainUI.SetActive(false);
        if (_loadingRoutine != null)
        {
            return;
        }
        _loadingRoutine = StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(sceneName);

        oper.allowSceneActivation = false;
        _loadingImage.gameObject.SetActive(true);
        _loadingBar.gameObject.SetActive(true);
        _loadingBG.gameObject.SetActive(true);

        while(oper.isDone == false)
        {
            if(oper.progress < 0.9f)
            {
                Debug.Log($"loading = {oper.progress}");
                _loadingBar.value = oper.progress;
            }
            else
            {
  
                break;
            }
            yield return null;
        }


        //Fake Loading
        float time = 0f;
        while(time < 5f)
        {
            time += Time.deltaTime;
            _loadingBar.value = time / 5f;
            yield return null;
        }

        Debug.Log("loading Success");
        oper.allowSceneActivation = true;
        _loadingImage.gameObject.SetActive(false);
    }


}
