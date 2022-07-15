using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    private Animator anim;
    public int goldAmount = 50;
    public GameObject particleEffect;
    public GameObject spawnPoint;
    public GameObject canvasText;
    public Text goldAmountText;
    public float speed = 1f;
    public GameObject mainCam;
    private int goldDisplay;
    public GameObject inventoryObj;
    public AudioClip openChest;
    public bool crate = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!crate)
        {
            anim = GetComponent<Animator>();
        }
        canvasText.SetActive(false);
        goldDisplay = goldAmount;
    }

    private void Update()
    {
        if (canvasText.activeSelf)
        {
            canvasText.transform.Translate(Vector3.up * speed * Time.deltaTime);
            goldAmountText.text = goldDisplay.ToString();
            canvasText.transform.LookAt(mainCam.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!crate)
        {
            if (other.CompareTag("Player"))
            {
                if (InventoryItems.key)
                {
                    anim.SetTrigger("open");
                    InventoryItems.gold += goldAmount;
                    goldAmount = 0;
                    inventoryObj.GetComponent<AudioSource>().clip = openChest;
                    inventoryObj.GetComponent<AudioSource>().Play();
                }
            }
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!crate)
        {
            if (other.CompareTag("Player"))
            {
                if (InventoryItems.key)
                {
                    anim.SetTrigger("close");
                }
            }
        }
    }

    public void DestroyChest()
    {
        Destroy(gameObject);
    }

    public void Particles()
    {
        Instantiate(particleEffect, spawnPoint.transform.position, spawnPoint.transform.rotation);
        canvasText.SetActive(true);
        if (crate)
        {
            InventoryItems.gold += goldAmount;
            goldAmount = 0;
            inventoryObj.GetComponent<AudioSource>().clip = openChest;
            inventoryObj.GetComponent<AudioSource>().Play();
        }
    }
}
