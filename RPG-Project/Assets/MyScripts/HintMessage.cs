using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HintMessage : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hintBox;
    public Text message;
    private bool displaying = true;
    private bool overIcon = false;
    public int objectType = 0;

    private Vector3 screenPoint;
    // Start is called before the first frame update
    void Start()
    {
        hintBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (overIcon)
        {
            if (Input.GetMouseButtonDown(0))
            {
                displaying = false;
                hintBox.SetActive(false);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            displaying = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        overIcon = true;
        if (displaying)
        {
            hintBox.SetActive(true);
            screenPoint.x = Input.mousePosition.x + 320;
            screenPoint.y = Input.mousePosition.y + 150;
            screenPoint.z = 1f;
            hintBox.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
            MessageDisplay();
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        overIcon = false;
        hintBox.SetActive(false);
    }

    void MessageDisplay()
    {
        if (objectType == 0)
        {
            message.text = "empty";
        }
        if (objectType == 1)
        {
            message.text = InventoryItems.redMushrooms.ToString() + " red mushrooms to be used in potions";
        }
        if (objectType == 2)
        {
            message.text = InventoryItems.purpleMushrooms.ToString() + " purple mushrooms to be used in potions";
        }
        if (objectType == 3)
        {
            message.text = InventoryItems.brownMushrooms.ToString() + " brown mushrooms to be used in potions";
        }
        if (objectType == 4)
        {
            message.text = InventoryItems.blueFlowers.ToString() + " blue flowers to be used in potions";
        }
        if (objectType == 5)
        {
            message.text = InventoryItems.redFlowers.ToString() + " red flowers to be used in potions";
        }
    }
}
