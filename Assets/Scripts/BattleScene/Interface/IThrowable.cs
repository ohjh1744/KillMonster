using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowable 
{
    public Vector3 Target { get; set; }
    public void Throw();
}
