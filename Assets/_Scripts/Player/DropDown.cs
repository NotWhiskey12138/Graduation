using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    public string oneWayPlatFormLayerName = "OneWayPlantForm";
    public string playerLayerName = "Player";

    public PlayerInputHandler inputHandler;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        if (inputHandler.NormInputY < 0)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(playerLayerName),LayerMask.NameToLayer(oneWayPlatFormLayerName),true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(playerLayerName),LayerMask.NameToLayer(oneWayPlatFormLayerName),false);

        }
    }
}
