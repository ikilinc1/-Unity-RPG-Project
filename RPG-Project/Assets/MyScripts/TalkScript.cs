using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkScript : MonoBehaviour
{
    public GameObject messageBox;
    public int tavernNumber = 0;
    public string answer;
    public GameObject question;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageBox.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageBox.SetActive(false);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageBox.GetComponentInChildren<MessageScript>().shopNumber = tavernNumber;
            question.GetComponent<MessageScript>().shopMessage = answer;
        }
    }
}
