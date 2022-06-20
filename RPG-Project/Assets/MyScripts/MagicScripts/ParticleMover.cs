using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMover : MonoBehaviour
{
    public GameObject target;
    public GameObject obj;
    public float speed = 5f;
    public float lifetime = 1.5f;
    
    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, target.transform.position, speed * Time.deltaTime);
        }   
        Destroy(obj, lifetime);
    }
}
