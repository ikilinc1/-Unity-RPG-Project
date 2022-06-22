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
    private GameObject targetSave;

    private void Start()
    {
        targetSave = SaveScript.theTarget;
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
        Destroy(obj, lifetime);
    }
}
