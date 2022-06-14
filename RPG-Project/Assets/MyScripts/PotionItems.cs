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

    public GameObject inventory;
    private bool check = true;
    
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
            thisImage.color = endColor;
            if (check)
            {
                check = false;
                inventory.GetComponent<InventoryItems>().currentId = objID;
                inventory.GetComponent<InventoryItems>().CheckStatics();
            }
        }
        if (theCanvas.GetComponent<CreatePotion>().thisValue == 0)
        {
            check = true;
            thisImage.color = startColor;
        }
    }
}
