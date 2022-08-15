using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DragonScript : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator anim;

    private AnimatorStateInfo enemyInfo;

    public GameObject player;
    private float distance;
    private bool isAttacking = false;
    public float closeAttackRange = 2.0f;
    public float farAttackRange = 15.0f;
    private float runRange = 50.0f;
    
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

    public GameObject fireball;
    public Transform fireballSpawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        thisEnemy.GetComponent<Outline>().enabled = false;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nav.avoidancePriority = 1;
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
           

            enemyInfo = anim.GetCurrentAnimatorStateInfo(0);

            distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < closeAttackRange || distance > runRange)
            {
                nav.isStopped = true;
                if (distance > runRange)
                {
                    SaveScript.enemiesOnScreen--;
                    Destroy(gameObject);
                }
                if (distance < closeAttackRange && enemyInfo.IsTag("nonAttack") && !anim.IsInTransition(0))
                {
                    if (!isAttacking)
                    {
                        isAttacking = true;
                        anim.SetTrigger("tailAttack");
                        
                        // Look at player smoothly
                        Vector3 pos = (player.transform.position - transform.position).normalized;
                        Quaternion posRotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
                        transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, Time.deltaTime * rotateSpeed);
                    }
                }
                if (distance < farAttackRange && distance > closeAttackRange && enemyInfo.IsTag("nonAttack") && !anim.IsInTransition(0))
                {
                    if (!isAttacking)
                    {
                        isAttacking = true;
                        anim.SetTrigger("fireAttack");
                        
                        // Look at player smoothly
                        Vector3 pos = (player.transform.position - transform.position).normalized;
                        Quaternion posRotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
                        transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, Time.deltaTime * rotateSpeed);
                    }
                }

                if (distance < farAttackRange && enemyInfo.IsTag("attack"))
                {
                    if (isAttacking)
                    {
                        isAttacking = false;
                    }
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


    public void ShootFireball()
    {
        fireballSpawnPoint.transform.LookAt(player.transform.position);
        Instantiate(fireball, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
    }
    
    IEnumerator IsDead()
    {
        yield return new WaitForSeconds(1);
        Instantiate(coins, transform.position, transform.rotation);
        Destroy(gameObject, 0.2f);
        SaveScript.killAmount++;
    }


}
