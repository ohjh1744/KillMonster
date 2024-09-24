using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] Image _loadingImage;
    [SerializeField] Slider _loadingBar;
    [SerializeField] Image _loadingBG;
    [SerializeField] GameObject _mainUI;

    private Coroutine _loadingRoutine;
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
        while (Input.anyKeyDown == false)
        {
            yield return null;
        }

        oper.allowSceneActivation = true;
        _loadingImage.gameObject.SetActive(false);
    }


}
