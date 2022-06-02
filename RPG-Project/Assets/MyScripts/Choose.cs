using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choose : MonoBehaviour
{
    public GameObject[] characters;
    private int p = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Next()
    {
        if (p < characters.Length - 1)
        {
            characters[p].SetActive(false);
            p++;
            characters[p].SetActive(true);
        }
    }
    
    public void Back()
    {
        if (p > 0)
        {
            characters[p].SetActive(false);
            p--;
            characters[p].SetActive(true);
        }
    }
}
