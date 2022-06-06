using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public int number;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InvertoryItems.newIcon = number;
            InvertoryItems.iconUpdate = true;
            Destroy(gameObject);
        }
    }
}
