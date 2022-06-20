using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

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
    public CinemachineVirtualCamera playerCam;
    private Vector3 pos;
    private Vector3 currPos;

    public static bool canMove = true;
    public static bool moving = false;
    public LayerMask moveLayer;

    public GameObject freeCam;
    public GameObject staticCam;
    private bool freeCamActive = true;

    public GameObject spawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        ct = playerCam.GetCinemachineComponent<CinemachineTransposer>();
        currPos = ct.m_FollowOffset;
        freeCam.SetActive(true);
        staticCam.SetActive(false);
        SaveScript.spawnPoint = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate velocity
        var velocity = nav.velocity;
        x = velocity.x;
        z = velocity.z;
        velocitySpeed = x + z;
        
        // Get mouse position
        pos = Input.mousePosition;
        ct.m_FollowOffset = currPos;
        //ct.m_FollowOffset = new Vector3(currPos.x, currPos.y * 3, currPos.x * 2);
        
        if (Input.GetMouseButtonDown(0))
        {
            if (canMove)
            {
                if (Camera.main != null) ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit,300,moveLayer))
                {
                    nav.destination = hit.point;
                }
            }
        }

        if (velocitySpeed != 0)
        {
            anim.SetBool("sprinting", true);
            moving = true;
        }

        if (velocitySpeed == 0)
        {
            anim.SetBool("sprinting", false);
            moving = false;
        }

        if (Input.GetMouseButton(1))
        {
            if (pos.x != 0 || pos.y != 0)
            {
                currPos = pos / 50;
            }
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
    }
}
