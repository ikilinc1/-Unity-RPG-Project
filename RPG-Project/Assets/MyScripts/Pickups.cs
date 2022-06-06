using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public int number;
    public bool redMushroom = false;
    public bool purpleMushroom = false;
    public bool brownMushroom = false;
    public bool blueFlower = false;
    public bool redFlower = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (redMushroom)
            {
                if (InvertoryItems.redMushrooms == 0)
                {
                    DisplayIcons();
                }
                InvertoryItems.redMushrooms++;
                Destroy(gameObject);
            }
            else if (purpleMushroom)
            {
                if (InvertoryItems.purpleMushrooms == 0)
                {
                    DisplayIcons();
                }
                InvertoryItems.purpleMushrooms++;
                Destroy(gameObject);
            }
            else if (brownMushroom)
            {
                if (InvertoryItems.brownMushrooms == 0)
                {
                    DisplayIcons();
                }
                InvertoryItems.brownMushrooms++;
                Destroy(gameObject);
            }
            else if (blueFlower)
            {
                if (InvertoryItems.blueFlowers == 0)
                {
                    DisplayIcons();
                }
                InvertoryItems.blueFlowers++;
                Destroy(gameObject);
            }
            else if (redFlower)
            {
                if (InvertoryItems.redFlowers == 0)
                {
                    DisplayIcons();
                }
                InvertoryItems.redFlowers++;
                Destroy(gameObject);
            }
            else
            {
                DisplayIcons();
                Destroy(gameObject);
            }
        }
    }

    void DisplayIcons()
    {
        InvertoryItems.newIcon = number;
        InvertoryItems.iconUpdate = true;
    }
}
