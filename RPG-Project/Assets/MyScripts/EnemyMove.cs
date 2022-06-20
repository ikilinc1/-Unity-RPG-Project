using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public GameObject thisEnemy;
    private bool outlineOn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        thisEnemy.GetComponent<Outline>().enabled = false;
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
    }
}
