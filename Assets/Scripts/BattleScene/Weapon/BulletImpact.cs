using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    [SerializeField] private float _remainTime;
    private float _currentTime;
    private void Update()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime > _remainTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _currentTime = 0;
    }
}
