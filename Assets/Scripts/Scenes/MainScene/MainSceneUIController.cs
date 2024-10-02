using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUIController: MonoBehaviour
{
    [SerializeField] private InputField _saveInputField;

    [SerializeField] private InputField _loadInputField;

    [SerializeField] private TextMeshProUGUI _saveText;

    [SerializeField] private TextMeshProUGUI _loadText;

    [SerializeField] private float _textActivefalseTime;

    private StringBuilder sb = new StringBuilder();

    private Coroutine _coroutine;

    private WaitForSeconds _textActiveFalseSeconds;

    void Start()
    {
        _saveInputField.onSubmit.AddListener(DataManager.Instance.Save);
        _saveInputField.onSubmit.AddListener(ShowSaveText);
        _loadInputField.onSubmit.AddListener(DataManager.Instance.Load);
        _loadInputField.onSubmit.AddListener(ShowLoadText);
        _textActiveFalseSeconds = new WaitForSeconds(_textActivefalseTime);
    }

    private void ShowSaveText(string st)
    {
        sb.Clear();
        if (DataManager.Instance.SaveState == ESaveState.Success)
        {
            sb.Append("Save Complete");
        }
        else if(DataManager.Instance.SaveState == ESaveState.alreadySave)
        {
            sb.Append("Save Already");
        }
        else if(DataManager.Instance.SaveState == ESaveState.Fail)
        {
            sb.Append("Save Failed");
        }
        _saveText.SetText(sb);
        _coroutine = StartCoroutine(ClearText());
    }

    private void ShowLoadText(string st)
    {
        sb.Clear();
        if (DataManager.Instance.LoadState == ELoadState.Success)
        {
            sb.Append("Load Complete");
        }
        else if(DataManager.Instance.LoadState == ELoadState.Fail)
        {
            sb.Append("Load Failed");
        }
        _loadText.SetText(sb);
        _coroutine = StartCoroutine(ClearText());
    }

    private IEnumerator ClearText()
    {
        yield return _textActiveFalseSeconds;

        sb.Clear();
        _saveText.SetText(sb);
        _loadText.SetText(sb);
        _coroutine = null;
        
    }



}
