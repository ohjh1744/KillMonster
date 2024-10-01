using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable 
{
    public int Bullet { get; }
    public float ReCoil { get;  }
    public bool IsReLoad { get; set; }
    public void Shoot(float damage);
    public void ReLoad();

}
