using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.LucidEditor;
using Unity.VisualScripting;
using Whiskey.CoreSystem;
using Whiskey.Interaction;

public class Chest : MonoBehaviour,IInteractable
{
    private InteractableDetector interactableDetector;
    
    public Animator animator;
    
    public bool IsOpened
    {
        get { return isOpened; }
        set
        {
            isOpened = value;
            animator.SetBool("IsOpened", isOpened);
        }
    }
    private bool isOpened;

    public void Open(IInteractable interactable)
    {
        IsOpened = true;
    }

    public void Close()
    {
        IsOpened = false;
    }
    

    public void EnableInteraction()
    {
        
    }

    public void DisableInteraction()
    {
        
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Interact()
    {
        Debug.Log("open chest");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactableDetector = other.GetComponentInChildren<InteractableDetector>();
            if (interactableDetector != null)
            {
                interactableDetector.OnTryInteract += Open;
            }
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactableDetector = other.GetComponentInChildren<InteractableDetector>();
            if (interactableDetector != null)
            {
                interactableDetector.OnTryInteract -= Open;
            }
        }
    }
    
}