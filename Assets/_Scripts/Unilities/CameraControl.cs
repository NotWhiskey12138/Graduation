using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;

    public CinemachineImpulseSource impulseSource;

    private void Start()
    {
        GetNewCameraBounds();
    }

    private void Awake()
    {
        //confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void GetNewCameraBounds()
    {
        //var obj = GameObject.FindGameObjectWithTag("Bounds");
        //if (obj = null) 
            //return;

        //confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        
        //confiner2D.InvalidateCache();
    }

    public void OnCameraShake()
    {
        impulseSource.GenerateImpulse();
    }
}
