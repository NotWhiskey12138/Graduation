using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public void OnCloseButton()
    {
        gameObject.SetActive(false);
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("BGMValue", value);
    }
}
