using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    public static int pchar = 0;
    public static string pname = "player";
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
