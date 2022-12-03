using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundFXSlider;

    public AudioMixer musicMixer;
    public AudioMixer soundFXMixer;

    public void ChangeMusicVolume()
    {
        musicMixer.SetFloat("musicVol", musicSlider.value);
    }
    
    public void ChangeSoundFXVolume()
    {
        soundFXMixer.SetFloat("sfxVol", soundFXSlider.value);
    }
}
