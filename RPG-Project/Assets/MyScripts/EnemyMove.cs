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

    private WaitForSeconds lookTime = new WaitForSeconds(2);

    public int enemyHealth = 100;
    private int currentHealth;
    private bool isAlive = true;
    
    // Start is called before the first frame update
    void Start()
    {
        thisEnemy.GetComponent<Outline>().enabled = false;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nav.avoidancePriority = Random.Range(5, 75);
        currentHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
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
                        StartCoroutine(LookAtPlayer());
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

            if (currentHealth > enemyHealth)
            {
                anim.SetTrigger("hit");
                currentHealth = enemyHealth;
            }
        }

        if (enemyHealth <= 1 && isAlive)
        {
            isAlive = false;
            nav.isStopped = true;
            anim.SetTrigger("death");
            thisEnemy.GetComponent<Outline>().enabled = false;
            outlineOn = false;
            nav.avoidancePriority = 1;
        }
    }

    IEnumerator LookAtPlayer()
    {
        yield return lookTime;
        if (isAlive)
        {
            transform.LookAt(player.transform);
        }
    }
}
