using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SaveZone : MonoBehaviour
{
    public GameObject saveScreen;
    public GameObject saveText;


    private bool savePause = false;
    
    // Start is called before the first frame update
    void Start()
    {
        saveScreen.SetActive(false);
        saveText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !savePause)
        {
            saveScreen.SetActive(true);
            Time.timeScale = 0;
            savePause = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && savePause)
        {
            savePause = false;
        }
    }

    public void ChooseYes()
    {
        SaveScript.saving = true;
        saveText.SetActive(true);
        StartCoroutine(Continue());
    }
    
    public void ChooseNo()
    {
        Time.timeScale = 1;
        saveScreen.SetActive(false);
    }

    IEnumerator Continue()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 1;
        saveScreen.SetActive(false);
        saveText.SetActive(false);
    }
}
