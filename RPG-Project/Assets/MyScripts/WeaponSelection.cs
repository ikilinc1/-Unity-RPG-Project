using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    public int weaponNumber;
    public AudioSource audioPlayer;
    public AudioClip selection;

    public void ChooseWeapon()
    {
        SaveScript.weaponChoice = weaponNumber;
        SaveScript.weaponChange = true;
        SaveScript.carryingWeapon = true;
        audioPlayer.clip = selection;
        audioPlayer.Play();
    }
}
