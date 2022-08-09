using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

    public int enemyHealth = 100;
    private int currentHealth;
    private bool isAlive = true;

    private AudioSource audioPlayer;
    public Image healthBar;
    private float fillHealth;
    public GameObject mainCam;

    public float rotateSpeed = 40.0f;

    public GameObject coins;
    
    // Start is called before the first frame update
    void Start()
    {
        thisEnemy.GetComponent<Outline>().enabled = false;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nav.avoidancePriority = Random.Range(5, 75);
        currentHealth = enemyHealth;
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCam == null)
        {
            mainCam = GameObject.Find("Main Camera");
        }
        healthBar.transform.LookAt(mainCam.transform.position);
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
                if (distance < attackRange && enemyInfo.IsTag("nonAttack") && !anim.IsInTransition(0))
                {
                    if (!isAttacking)
                    {
                        isAttacking = true;
                        anim.SetTrigger("attack");
                        
                        // Look at player smoothly
                        Vector3 pos = (player.transform.position - transform.position).normalized;
                        Quaternion posRotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
                        transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, Time.deltaTime * rotateSpeed);
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
            else if (distance > attackRange && enemyInfo.IsTag("nonAttack") && !anim.IsInTransition(0))
            {
                if (!SaveScript.invisible)
                {
                    nav.isStopped = false;
                    nav.destination = player.transform.position;
                }
            }

            if (currentHealth > enemyHealth)
            {
                anim.SetTrigger("hit");
                currentHealth = enemyHealth;
                audioPlayer.Play();
                fillHealth = enemyHealth;
                fillHealth /= 100.0f;
                healthBar.fillAmount = fillHealth;
            }
        }

        if (enemyHealth <= 1 && isAlive)
        {
            isAlive = false;
            nav.isStopped = true;
            anim.SetTrigger("death");
            SaveScript.enemiesOnScreen--;
            thisEnemy.GetComponent<Outline>().enabled = false;
            outlineOn = false;
            nav.avoidancePriority = 1;
            StartCoroutine(IsDead());
        }
    }


    IEnumerator IsDead()
    {
        yield return new WaitForSeconds(1);
        Instantiate(coins, transform.position, transform.rotation);
        Destroy(gameObject, 0.2f);
        SaveScript.killAmount++;
    }


}
