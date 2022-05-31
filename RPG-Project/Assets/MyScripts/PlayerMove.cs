using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator anim;
    private Ray ray;
    private RaycastHit hit;

    private float x;
    private float z;
    private float velocitySpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate velocity
        var velocity = nav.velocity;
        x = velocity.x;
        z = velocity.z;
        velocitySpeed = x + z;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main != null) ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                nav.destination = hit.point;
            }
        }

        if (velocitySpeed != 0)
        {
            anim.SetBool("sprinting", true);
        }

        if (velocitySpeed == 0)
        {
            anim.SetBool("sprinting", false);
        }
    }
}
