using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    private void OnDisable()
    {
        OffSafe();
    }
    void OffSafe()
    {
        _playerData.IsSafe = false;
    }
}
