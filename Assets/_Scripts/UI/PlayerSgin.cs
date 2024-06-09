using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class PlayerSgin : MonoBehaviour
{
    private Animator anim;

    public Transform playerTrans;
    
    public GameObject signSprite; 
    
    private bool canPress;

    private void Awake()
    {
        anim = signSprite.GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.Log(canPress);
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
        signSprite.transform.localRotation = playerTrans.localRotation;
        
    }

    private void OnEnable()
    {
        InputSystem.onActionChange += OnActionChange;
    }

    private void OnDisable()
    {
        InputSystem.onActionChange -= OnActionChange;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            canPress = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canPress = false;
    }

    private void OnActionChange(object obj, InputActionChange actionChange)
    {
        if (actionChange == InputActionChange.ActionStarted)
        {
            Debug.Log(((InputAction)obj).activeControl.device);

            var d = ((InputAction)obj).activeControl.device;

            switch (d.device)
            {
                case Keyboard:
                    anim.Play("Keyboard");
                    break;
                case DualShockGamepad:
                    anim.Play("PS");
                    break;
            }

        }
    }
}
