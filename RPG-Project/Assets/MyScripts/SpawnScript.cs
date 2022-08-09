using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public GameObject myCamera;

    private bool canSpawn = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canSpawn)
            {
                canSpawn = false;
                for (int i = 0; i < enemies.Length; i++)
                {
                    Instantiate(enemies[i], spawnPoints[i].position, spawnPoints[i].rotation);
                    SaveScript.enemiesOnScreen++;
                    myCamera.GetComponent<AudioManager>().musicState = 3;
                    myCamera.GetComponent<AudioManager>().canPlay = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.enemiesOnScreen <= 0)
        {
            if (!canSpawn)
            {
                canSpawn = true;
                myCamera.GetComponent<AudioManager>().musicState = 1;
                myCamera.GetComponent<AudioManager>().canPlay = true;
            }
        }
    }
}
