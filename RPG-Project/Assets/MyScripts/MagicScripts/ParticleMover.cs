using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMover : MonoBehaviour
{
    public GameObject target;
    public GameObject obj;
    public float speed = 5f;
    public float lifetime = 1.5f;
    public bool enemySeeker = false;
    public bool nonMoving = false;
    public bool followPlayer = false;
    
    public float manaCost = 0.05f;
    public bool invisibility = false;
    public bool invulnerability = false;
    public bool healing = false;
    public bool strength = false;
    
    public int damageAmount = 30;
    public GameObject lastObj;
    
    private GameObject targetSave;
    private GameObject playerObj;

    private void Start()
    {
        targetSave = SaveScript.theTarget;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if (invisibility)
        {
            SaveScript.invisible = true;
        }

        if (invulnerability)
        {
            SaveScript.invulnerable = true;
        }

        if (healing)
        {
            SaveScript.playerHealth = 1.0f;
        }

        if (strength)
        {
            SaveScript.strengthIncrease = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        if (enemySeeker)
        {
            if (targetSave)
            {
                transform.position = Vector3.LerpUnclamped(transform.position, targetSave.transform.position, speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
        if (nonMoving)
        {
            if (targetSave)
            {
                transform.position = targetSave.transform.position;
            }
            else
            {
                Destroy(obj);
            }
        }

        if (followPlayer)
        {
            transform.position = playerObj.transform.position;
            lifetime = 100;
            if (SaveScript.manaAmount <= 0.02f)
            {
                Destroy(obj);
            }
        }

        SaveScript.manaAmount -= manaCost * Time.deltaTime;
        Destroy(obj, lifetime);
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
