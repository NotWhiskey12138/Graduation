using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
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