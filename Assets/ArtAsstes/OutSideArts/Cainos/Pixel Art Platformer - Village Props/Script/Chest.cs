using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.LucidEditor;
using Whiskey;
using Whiskey.Interaction;

public class Chest : MonoBehaviour,IInteractable
{
    [FoldoutGroup("Reference")]
    public Animator animator;

    [SerializeField] private Bobber bobber;
    
    [FoldoutGroup("Runtime"), ShowInInspector, DisableInEditMode]
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

    [FoldoutGroup("Runtime"),Button("Open"), HorizontalGroup("Runtime/Button")]
    public void Open()
    {
        IsOpened = true;
    }

    [FoldoutGroup("Runtime"), Button("Close"), HorizontalGroup("Runtime/Button")]
    public void Close()
    {
        IsOpened = false;
    }

    public void Interact()
    {
        if (isOpened == false) 
        {
            Open();
        }
        else
        {
            return;
        }
    }

    public void EnableInteraction()
    {
        bobber.StartBobbing();
    }

    public void DisableInteraction()
    {
        bobber.StopBobbing();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    
}    

