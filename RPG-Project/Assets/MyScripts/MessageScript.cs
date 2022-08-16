using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessageScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text buttonText;
    public Text shopOwnerMessage;
    public Color32 messageOff;
    public Color32 messageOn;

    public GameObject[] shopUI;
    
    [HideInInspector]
    public int shopNumber = 0;

    public string shopMessage;
    public GameObject inventoryObj;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = messageOn;
        PlayerMove.canMove = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = messageOff;
        PlayerMove.canMove = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        shopOwnerMessage.text = "Hello " + SaveScript.pname + ", How can i help you?";
    }

    public void Message1()
    {
        shopOwnerMessage.text = shopMessage;
        if (inventoryObj)
        {
            inventoryObj.GetComponent<InventoryItems>().UpdateMessages(shopMessage);
        }
    }
    
    public void Message2()
    {
        shopOwnerMessage.text = "Sure, as long as you have some coins.";
        shopUI[shopNumber].SetActive(true);
        if (shopNumber < 6)
        {
            shopUI[shopNumber].GetComponent<BuyScript>().UpdateGold();
        }
    }

    private void Update()
    {
        if (PlayerMove.canMove && PlayerMove.moving)
        {
            if (shopUI[shopNumber])
            {
                shopOwnerMessage.text = "Hello " + SaveScript.pname + ", How can i help you?";
                shopUI[shopNumber].SetActive(false);
            }
        }
        
    }
}
