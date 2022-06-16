using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientPlay : MonoBehaviour
{
    private AudioSource audioPlayer;
    public WaitForSeconds waitTime = new WaitForSeconds(4);
    
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        StartCoroutine(AnimalSound());
    }

    IEnumerator AnimalSound()
    {
        yield return waitTime;
        audioPlayer.Play();
        StartCoroutine(AnimalSound());
    }
}
