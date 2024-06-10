using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Whiskey.CoreSystem;
using Whiskey.Interaction;

public class Level1ExitGame : MonoBehaviour,IInteractable
{
    
    private InteractableDetector interactableDetector;

    [SerializeField] private GameObject finishPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    
    public void FinishLevel1(IInteractable interactable)
    {
        finishPanel.SetActive(true);
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactableDetector = other.GetComponentInChildren<InteractableDetector>();
            if (interactableDetector != null)
            {
                interactableDetector.OnTryInteract += FinishLevel1;
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
                interactableDetector.OnTryInteract -= FinishLevel1;
            }
        }
    }
}
