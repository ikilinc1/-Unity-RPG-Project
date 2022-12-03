using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundFXSlider;

    public AudioMixer musicMixer;
    public AudioMixer soundFXMixer;

    private GameObject saveObj;
    
    public void ChangeMusicVolume()
    {
        musicMixer.SetFloat("musicVol", musicSlider.value);
    }
    
    public void ChangeSoundFXVolume()
    {
        soundFXMixer.SetFloat("sfxVol", soundFXSlider.value);
    }

    public void BackToMenu()
    {
        SaveScript.playerHealth = 1.0f;
        SaveScript.instance = 0;
        saveObj = GameObject.Find("SaveObject");
        Destroy(saveObj);
    }
}
