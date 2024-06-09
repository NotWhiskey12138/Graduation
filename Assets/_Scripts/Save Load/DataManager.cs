using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private List<ISaveable> saveableList = new List<ISaveable>();

    
    private Data saveData;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (saveData == null)
        {
            saveData = new Data();
        }
        else
        {
            saveData = this.saveData;
        }
        
    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Debug.Log("L press");
            Load();
        }
        
    }

    public void RegisterSaveData(ISaveable saveable)
    {
        if (!saveableList.Contains(saveable))
        {
            saveableList.Add(saveable);
        }
    }

    public void UnRegisterSaveData(ISaveable saveable)
    {
        saveableList.Remove(saveable);
    }

    public void Save()
    {
        Debug.Log("save");
        
        foreach (var saveable in saveableList)
        {
            saveable.GetSaveData(saveData);
        }

        foreach (var item in saveData.characterPosDict)
        {
            Debug.Log(item.Key + "  " + item.Value);
        }
    }

    public void Load()
    {
        Debug.Log("load");
        
        foreach (var saveable in saveableList)
        {
            saveable.LoadData(saveData);
        }
    }



}
