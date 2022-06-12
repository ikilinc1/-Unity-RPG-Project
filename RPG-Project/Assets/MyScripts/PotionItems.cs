using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionItems : MonoBehaviour
{
    public GameObject theCanvas;
    public int objID;
    
    [HideInInspector]
    public Image thisImage;
    [HideInInspector]
    public Color32 startColor = new Color32(255,255,255,120);
    [HideInInspector]
    public Color32 endColor = new Color32(255,255,255,255);
    // Start is called before the first frame update
    void Start()
    {
        thisImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theCanvas.GetComponent<CreatePotion>().thisValue == objID)
        {
            // maybe lerp color here later 
            thisImage.color = endColor;
        }
        if (theCanvas.GetComponent<CreatePotion>().thisValue == 0)
        {
            thisImage.color = startColor;
        }
    }
}
