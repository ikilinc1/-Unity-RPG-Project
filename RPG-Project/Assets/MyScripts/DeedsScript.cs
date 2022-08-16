using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeedsScript : MonoBehaviour
{
    public GameObject inventoryObj;
    public Text[] deeds;
    public bool canUpdate = false;
    

    // Update is called once per frame
    void Update()
    {
        if (canUpdate)
        {
            canUpdate = false;
            for (int i = 0; i < inventoryObj.GetComponent<InventoryItems>().messages.Length; i++)
            {
                if (inventoryObj.GetComponent<InventoryItems>().messages[i].text != "blank")
                {
                    deeds[i].text = inventoryObj.GetComponent<InventoryItems>().messages[i].text;
                    deeds[i].color = new Color(255, 255, 255, 255);
                }
            }
        }
        
    }
}
