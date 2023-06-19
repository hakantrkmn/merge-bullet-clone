using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera mergeCam;

    private void OnEnable()
    {
        EventManager.ShotButtonClicked += ShooterPhase;
    }

    private void OnDisable()
    {
        EventManager.ShotButtonClicked -= ShooterPhase;
    }
    
    private void ShooterPhase()
    {
        playerCam.Priority = 10;
        mergeCam.Priority = 0;
    }
}
