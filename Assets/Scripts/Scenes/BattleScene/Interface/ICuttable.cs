using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICuttable 
{
    public void Cut(Vector3 pos, float damage, AudioSource audioSource);
}
