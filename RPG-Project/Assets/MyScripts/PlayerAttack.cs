using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damageAmount;
    private GameObject objToDestroy;
    private bool canDamage = true;
    private WaitForSeconds damagePause = new WaitForSeconds(0.5f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crate"))
        {
            other.transform.gameObject.GetComponentInParent<ChestScript>().Particles();
            objToDestroy = other.transform.parent.gameObject;
            Destroy(other.transform.gameObject);
            StartCoroutine(WaitForDestroy());
        }

        if (other.CompareTag("enemy") && canDamage)
        {
            canDamage = false;
            other.transform.gameObject.GetComponent<EnemyMove>().enemyHealth -= damageAmount + SaveScript.weaponIncrease + SaveScript.strengthIncrease;
            StartCoroutine(ResetDamage());
        }
        if (other.CompareTag("spider") && canDamage)
        {
            canDamage = false;
            other.transform.gameObject.GetComponent<EnemyMove>().enemyHealth -= (damageAmount / 8) + SaveScript.weaponIncrease + SaveScript.strengthIncrease;
            StartCoroutine(ResetDamage());
        }
        if (other.CompareTag("dragon") && canDamage)
        {
            canDamage = false;
            other.transform.gameObject.GetComponent<DragonScript>().enemyHealth -= (damageAmount / 8) + SaveScript.weaponIncrease + SaveScript.strengthIncrease;
            StartCoroutine(ResetDamage());
        }
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(objToDestroy);
    }
    
    IEnumerator ResetDamage()
    {
        yield return damagePause;
        canDamage = true;
    }
}
