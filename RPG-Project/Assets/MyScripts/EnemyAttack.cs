using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damageAmount = 0.006f;
    private bool canAttack = true;
    private WaitForSeconds delayTime = new WaitForSeconds(1);
    private AudioSource audioPlayer;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canAttack && !SaveScript.invulnerable)
            {
                canAttack = false;
                SaveScript.playerHealth -= damageAmount - SaveScript.armorValue;
                audioPlayer.Play();
                StartCoroutine(ResetDamage());
            }
        }
    }

    IEnumerator ResetDamage()
    {
        yield return delayTime;
        canAttack = true;
    }
}
