using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTarget : MonoBehaviour
{
    public float speed = 1.0f;
    public bool rotator = false;
    public bool particleTarget = true;
    public int damageAmount = 30;
    public GameObject lastObj;

    // Update is called once per frame
    void Update()
    {
        if (rotator)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }

        if (particleTarget)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy") && other.transform.gameObject != lastObj)
        {
            other.transform.gameObject.GetComponent<EnemyMove>().enemyHealth -= damageAmount;
            lastObj = other.transform.gameObject;
        }
    }
}
