using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject[] characters;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(characters[SaveScript.pchar], spawnPoint.position, spawnPoint.rotation);
    }
}
