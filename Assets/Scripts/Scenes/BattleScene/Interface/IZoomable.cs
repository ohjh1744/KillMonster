using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZoomable 
{
    void ZoomIn(CinemachineVirtualCamera _virtualCamera);
    void ZoomOut(CinemachineVirtualCamera camera_virtualCamera);

}
