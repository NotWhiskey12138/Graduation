using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemaControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D; //摄像机边界

    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void Start()
    {
        GetNewCameraBounds();
    }

    //TODO：需要完成场景切换
    private void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");

        if (obj == null) return;

        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        
        confiner2D.InvalidateCache(); //每切换一次Collider就清理一次缓存。
    }
}
