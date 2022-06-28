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
    
    private bool magicCollected = false;
    private bool spellsCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        spellsUI.SetActive(false);
        magicUI.SetActive(false);
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
                    Destroy(gameObject);
                }
            }
            
            if (spellsbook)
            {
                if (!spellsCollected)
                {
                    spellsUI.SetActive(true);
                    spellsCollected = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}
