using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkScript : MonoBehaviour
{
    public GameObject messageBox;
    public int tavernNumber = 0;
    public string answer;
    public GameObject question;
    

    private bool haveRead = false;
    private GameObject minimapView;
    private GameObject minimapCompass;

    private void Start()
    {
        minimapView = GameObject.FindGameObjectWithTag("minimapItem");
        minimapCompass = GameObject.FindGameObjectWithTag("compass");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageBox.SetActive(true);
            minimapView.SetActive(false);
            minimapCompass.SetActive(false);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageBox.SetActive(false);
            minimapView.SetActive(true);
            minimapCompass.SetActive(true);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageBox.GetComponentInChildren<MessageScript>().shopNumber = tavernNumber;
            if (!haveRead)
            {
                haveRead = false;
                question.GetComponent<MessageScript>().shopMessage = answer;
                StartCoroutine(FirstEntry());
            }
            else if(haveRead)
            {
                question.GetComponent<MessageScript>().shopMessage = "Not much going on around here.";
            }
        }
    }

    IEnumerator FirstEntry()
    {
        yield return new WaitForSeconds(1);
        haveRead = true;
    }
}
