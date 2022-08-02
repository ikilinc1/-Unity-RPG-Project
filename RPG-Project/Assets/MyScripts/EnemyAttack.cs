using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damageAmount = 0.1f;
    private bool canAttack = true;
    private WaitForSeconds delayTime = new WaitForSeconds(1);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canAttack)
            {
                canAttack = false;
                SaveScript.playerHealth -= damageAmount;
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
