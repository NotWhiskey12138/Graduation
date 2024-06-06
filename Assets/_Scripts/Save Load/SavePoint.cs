using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Whiskey;
using Whiskey.Interaction;

public class SavePoint : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject enviormentLight;
    [SerializeField] private Bobber bobber;

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

    public void Interact()
    {
        Destroy(gameObject);
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

    public void UseItem()
    {
        if (!isDone)
        {
            isDone = true;

            this.gameObject.tag = "Untagged";
            
            //保存数据
            DataManager.instance.Save();
        }
    }
}
