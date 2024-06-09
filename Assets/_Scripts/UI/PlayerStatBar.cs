using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;

    public Image healthDelayImage;

    private void Update()
    {
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 血量改变的时候
    /// </summary>
    /// <param name="persentage"></param>
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }
    
}
