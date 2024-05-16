using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get; private set; }
    public CollisionSenses CollisionSenses { get; private set; }

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();

        if (!Movement)
        {
            Debug.LogError("Missing Core Movement Component");
        }

        if (!CollisionSenses)
        {
            Debug.LogError("Missing Core CollisionSenses Component");
        }
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }
}