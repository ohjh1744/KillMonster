using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadInputField : MonoBehaviour
{
    [SerializeField] private InputField _saveInputField;
    [SerializeField] private InputField _loadInputField;
    void Start()
    {
        _saveInputField.onSubmit.AddListener(DataManager.Instance.Save);
        _loadInputField.onSubmit.AddListener(DataManager.Instance.Load);
    }

}
