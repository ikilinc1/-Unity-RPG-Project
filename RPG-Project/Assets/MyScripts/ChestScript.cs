using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private Animator anim;
    public int goldAmount = 50;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventoryItems.key)
            {
                anim.SetTrigger("open");
                InventoryItems.gold += goldAmount;
                goldAmount = 0;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventoryItems.key)
            {
                anim.SetTrigger("close");
            }
        }
    }

    public void DestroyChest()
    {
        Destroy(gameObject);
    }
}
