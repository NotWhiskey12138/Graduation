using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Whiskey.CoreSystem;
using Whiskey.CoreSystem.StatsSystem;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;

    public Stat playerStats;

    private void Awake()
    {
        if (playerStats == null)
        {
            Debug.LogError("Player Stats Component is null!!!");
            return;
        }
    }

    private void Update()
    {
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime;
        }
        
        PlayerHealthChange();
    }

    public void PlayerHealthChange()
    {
        var health = GetComponent<PlayerStatBar>();
        health.OnHealthChange(playerStats.GetCurrentHealth() / playerStats.GetMaxHealth());
    }
    
    /// <summary>
    /// 接收Health的变更百分比
    /// </summary>
    /// <param name="persentage">百分比:Current/Max</param>
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }
}
