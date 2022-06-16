using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofScript : MonoBehaviour
{
    public GameObject roof;
    public GameObject props;
    public GameObject myCamera;
    public bool tavern = true;
    
    // Start is called before the first frame update
    void Start()
    {
        roof.SetActive(true);
        props.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roof.SetActive(false);
            props.SetActive(true);
            if (tavern)
            {
                myCamera.GetComponent<AudioManager>().musicState = 2;
                myCamera.GetComponent<AudioManager>().canPlay = true;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roof.SetActive(true);
            props.SetActive(false);
            if (tavern)
            {
                myCamera.GetComponent<AudioManager>().musicState = 1;
                myCamera.GetComponent<AudioManager>().canPlay = true;
            }
        }
    }
    
}
