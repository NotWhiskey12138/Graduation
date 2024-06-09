using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Whiskey.CoreSystem;
using Whiskey.Interaction;

public class SavePoint : MonoBehaviour, IInteractable
{
    private InteractableDetector interactableDetector;
    
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject enviormentLight;

    public bool isDone;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        spriteRenderer.enabled = isDone;
        enviormentLight.SetActive(isDone);
    }


    public void UseItem(IInteractable interactable)
    {
        if (!isDone)
        {
            isDone = true;

            this.gameObject.tag = "Untagged";
            
            //保存数据
            DataManager.instance.Save();
        }
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
        
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactableDetector = other.GetComponentInChildren<InteractableDetector>();
            if (interactableDetector != null)
            {
                interactableDetector.OnTryInteract += UseItem;
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
                interactableDetector.OnTryInteract -= UseItem;
            }
        }
    }
    
}
