using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCollect : MonoBehaviour
{
    public GameObject magicUI;
    public GameObject spellsUI;

    public bool magicBook = false;
    public bool spellsbook = false;
    
    public static bool magicCollected = false;
    public static bool spellsCollected = false;

    public GameObject spellsMessage;
    public GameObject magicMessage;
    public GameObject inventoryObj;
    public AudioClip openBook;
    // Start is called before the first frame update
    void Start()
    {
        if (magicBook)
        {
            magicUI.SetActive(false);
            magicMessage.SetActive(false);
        }
        if (spellsbook)
        {
            spellsUI.SetActive(false);
            spellsMessage.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (magicBook)
            {
                if (!magicCollected)
                {
                    magicUI.SetActive(true);
                    magicCollected = true;
                    StartCoroutine(DisplayMessageUI());
                    //Destroy(gameObject);
                }
            }
            
            if (spellsbook)
            {
                if (!spellsCollected)
                {
                    spellsUI.SetActive(true);
                    spellsCollected = true;
                    StartCoroutine(DisplayMessageUI());
                    //Destroy(gameObject);
                }
            }
        }
    }

    IEnumerator DisplayMessageUI()
    {
        yield return new WaitForSeconds(0.5f);
        inventoryObj.GetComponent<AudioSource>().clip = openBook;
        inventoryObj.GetComponent<AudioSource>().Play();
        if (magicBook)
        {
            magicMessage.SetActive(true);
        }

        if (spellsbook)
        {
            spellsMessage.SetActive(true);
        }

        yield return new WaitForSeconds(2);
        
        if (magicBook)
        {
            magicMessage.SetActive(false);
        }

        if (spellsbook)
        {
            spellsMessage.SetActive(false);
        }
        
        Destroy(gameObject);
    }
}
