using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    
    
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindPlayer());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = player.transform.position;
        pos.y = transform.position.y;
        transform.position = pos;
    }

    IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(1);
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
