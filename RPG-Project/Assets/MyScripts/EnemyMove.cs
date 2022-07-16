using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator anim;
    
    private float x;
    private float z;
    private float velocitySpeed;
    
    private AnimatorStateInfo enemyInfo;

    public GameObject player;
    private float distance;
    private bool isAttacking = false;
    public float attackRange = 2.0f;
    public float runRange = 12.0f;
    
    public GameObject thisEnemy;
    private bool outlineOn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        thisEnemy.GetComponent<Outline>().enabled = false;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!outlineOn)
        {
            outlineOn = true;
            if (SaveScript.theTarget == thisEnemy)
            {
                thisEnemy.GetComponent<Outline>().enabled = true;
            }
        }
        if (outlineOn)
        {
            if (SaveScript.theTarget != thisEnemy)
            {
                thisEnemy.GetComponent<Outline>().enabled = false;
                outlineOn = false;
            }
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        // Calculate velocity
        var velocity = nav.velocity;
        x = velocity.x;
        z = velocity.z;
        velocitySpeed = x + z;

        if (velocitySpeed == 0)
        {
            anim.SetBool("running", false);
        }
        else
        {
            anim.SetBool("running", true);
            isAttacking = false;
        }

        enemyInfo = anim.GetCurrentAnimatorStateInfo(0);

        distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < attackRange || distance > runRange)
        {
            nav.isStopped = true;
            if (distance < attackRange && enemyInfo.IsTag("nonAttack"))
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    anim.SetTrigger("attack");
                }
            }

            if (distance < attackRange && enemyInfo.IsTag("attack"))
            {
                if (isAttacking)
                {
                    isAttacking = false;
                }
            }
        }
        else
        {
            nav.isStopped = false;
            nav.destination = player.transform.position;
        }
    }
}
