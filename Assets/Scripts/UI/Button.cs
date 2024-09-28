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
}
