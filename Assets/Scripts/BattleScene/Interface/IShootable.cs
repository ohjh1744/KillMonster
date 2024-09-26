using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable 
{
    public int Bullet { get; set; }
    public float ReCoil { get; set; }
    public bool IsReLoad { get; set; }
    public void Shoot(Camera camera);
    public void ReLoad();

    public GameObject FireFlash { get; set; }
}
