using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator anim;
    private Ray ray;
    private RaycastHit hit;

    private float x;
    private float z;
    private float velocitySpeed;

    private CinemachineTransposer ct;
    private CinemachineOrbitalTransposer cot;
    private Vector3 pos;
    private Vector3 currPos;
    private string axisName = "Mouse X";

    public static bool canMove = true;
    public static bool moving = false;
    public LayerMask moveLayer;

    public GameObject freeCam;
    public GameObject staticCam;
    private bool freeCamActive = true;

    public GameObject spawnPoint;
    private WaitForSeconds approachEnemy = new WaitForSeconds(0.3f);

    public GameObject[] playerObjs;
    public GameObject[] weapons;

    public GameObject[] armorTorso;
    public GameObject[] armorLegs;
    public string[] attacks;
    private AnimatorStateInfo playerInfo;
    public AudioSource audioPlayer;
    public AudioClip[] weaponSounds;
    
    private GameObject trailObj;
    private WaitForSeconds trailTimeOff = new WaitForSeconds(0.1f);
    public float[] staminaCost;

    private float currentHealth = 1.0f;
    public GameObject hitEffect;
    private WaitForSeconds hitOff = new WaitForSeconds(0.5f);

    public GameObject aimObj;
    
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //ct = freeCam.gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
        cot = staticCam.gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();
        //currPos = ct.m_FollowOffset;
        freeCam.SetActive(true);
        staticCam.SetActive(false);
        SaveScript.spawnPoint = spawnPoint;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        hitEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Listen to the animator
        if (anim)
        {
            playerInfo = anim.GetCurrentAnimatorStateInfo(0);
        }
        
        // Calculate velocity
        var velocity = nav.velocity;
        x = velocity.x;
        z = velocity.z;
        velocitySpeed = x + z;
        
        // Get mouse position
        pos = Input.mousePosition;
        //ct.m_FollowOffset = currPos;
        //ct.m_FollowOffset = new Vector3(currPos.x, currPos.y * 3, currPos.x * 2);

        if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                aimObj.transform.Rotate(0,75 * Time.deltaTime,0);
            }
            if (Input.GetAxis("Mouse X") < 0)
            {
                aimObj.transform.Rotate(0,-75 * Time.deltaTime,0);
            }
        }
        
        // Switch on correct weapon
        if (SaveScript.weaponChange)
        {
            SaveScript.weaponChange = false;
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[SaveScript.weaponChoice].SetActive(true);
            StartCoroutine(TurnOffTrail());
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (SaveScript.carryingWeapon && SaveScript.staminaAmount > 0.2f)
            {
                anim.SetTrigger(attacks[SaveScript.weaponChoice]);
                audioPlayer.clip = weaponSounds[SaveScript.weaponChoice];
                SaveScript.staminaAmount -= staminaCost[SaveScript.weaponChoice];
            }
        }
        
        if (Input.GetMouseButtonDown(0) && playerInfo.IsTag("nonAttack") && !anim.IsInTransition(0))
        {
            if (canMove)
            {
                if (Camera.main != null) ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit,300,moveLayer))
                {
                    if (hit.transform.gameObject.CompareTag("enemy"))
                    {
                        nav.isStopped = false;
                        SaveScript.theTarget = hit.transform.gameObject;
                        nav.destination = hit.point;
                        transform.LookAt(SaveScript.theTarget.transform);
                        StartCoroutine(MoveTo());
                    }
                    else
                    {
                        SaveScript.theTarget = null;
                        nav.destination = hit.point;
                        nav.isStopped = false;
                    }
                }
            }
        }

        if (velocitySpeed != 0)
        {
            if (!SaveScript.carryingWeapon)
            {
                anim.SetBool("sprinting", true);
                anim.SetBool("carryWeapon", false);
            }
            if (SaveScript.carryingWeapon)
            {
                anim.SetBool("sprinting", true);
                anim.SetBool("carryWeapon", true);
            }
            moving = true;
        }

        if (velocitySpeed == 0)
        {
            anim.SetBool("sprinting", false);
            moving = false;
        }

        if (Input.GetMouseButton(1))
        {
            cot.m_XAxis.m_InputAxisName = axisName;
            if (pos.x != 0 || pos.y != 0)
            {
                currPos = pos / 50;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            cot.m_XAxis.m_InputAxisName = null;
            cot.m_XAxis.m_InputAxisValue = 0;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (freeCamActive)
            {
                freeCam.SetActive(false);
                staticCam.SetActive(true);
                freeCamActive = false;
            }
            else if (!freeCamActive)
            {
                freeCam.SetActive(true);
                staticCam.SetActive(false);
                freeCamActive = true;
            }
        }

        if (playerObjs[0].activeSelf)
        {
            if (SaveScript.invisible)
            {
                for (int i = 0; i < playerObjs.Length; i++)
                {
                    playerObjs[i].SetActive(false);
                }
            }
        }
        
        if (SaveScript.manaAmount <= 0.1f)
        {
            if (!SaveScript.invisible)
            {
                for (int i = 0; i < playerObjs.Length; i++)
                {
                    playerObjs[i].SetActive(true);
                    SaveScript.changeArmor = true;
                }
            }
        }

        if (SaveScript.changeArmor)
        {
            for (int i = 0; i < armorTorso.Length; i++)
            {
                armorTorso[i].SetActive(false);
            }
            armorTorso[SaveScript.armor].SetActive(true);
            for (int i = 0; i < armorLegs.Length; i++)
            {
                armorLegs[i].SetActive(false);
            }
            armorLegs[SaveScript.armor].SetActive(true);

            SaveScript.changeArmor = false;
        }

        if (currentHealth > SaveScript.playerHealth)
        {
            hitEffect.SetActive(true);
            currentHealth = SaveScript.playerHealth;
            StartCoroutine(hitEffectOff());
        }
    }

    public void PlayWeaponSound()
    {
        audioPlayer.Play();
    }

    public void TrailOn()
    {
        trailObj.GetComponent<Renderer>().enabled = true;
    }
    
    public void TrailOff()
    {
        trailObj.GetComponent<Renderer>().enabled = false;
    }
    
    IEnumerator MoveTo()
    {
        yield return approachEnemy;
        nav.isStopped = true;
    }
    
    IEnumerator TurnOffTrail()
    {
        yield return trailTimeOff;
        trailObj = GameObject.Find("Trail");
        trailObj.GetComponent<Renderer>().enabled = false;
    }
    
    IEnumerator hitEffectOff()
    {
        yield return hitOff;
        hitEffect.SetActive(false);
    }
}
