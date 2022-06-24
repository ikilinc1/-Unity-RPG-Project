using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    public static int pchar = 0;
    public static string pname = "player";
    public static GameObject spawnPoint;
    public static GameObject theTarget;
    public static float manaAmount = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (manaAmount < 1f)
        {
            manaAmount += 0.04f * Time.deltaTime;
        }
        if (manaAmount <= 0)
        {
            manaAmount = 0;
        }
    }
}
