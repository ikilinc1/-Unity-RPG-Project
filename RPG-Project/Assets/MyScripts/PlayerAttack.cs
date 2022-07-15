using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damageAmount;
    private GameObject objToDestroy;
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
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(objToDestroy);
    }
}
