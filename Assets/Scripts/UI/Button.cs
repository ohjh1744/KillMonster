using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSfx;
    
    public void ClickButton()
    {
        SoundManager.Instance.PlaySFX(_clickSfx);
    }

    public void ActiveUI(GameObject gameObject)
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
