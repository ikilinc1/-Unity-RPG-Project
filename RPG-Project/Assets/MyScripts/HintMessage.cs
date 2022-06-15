using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HintMessage : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject hintBox;
    public Text message;
    private bool displaying = true;
    private bool overIcon = false;
    public int objectType = 0;

    private Vector3 screenPoint;
    public GameObject theCanvas;
    
    public Sprite cursorBasic;
    public Sprite cursorHand;
    public Image cursorImage;

    public GameObject inventoryObject;
    public bool magic = false;
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
                if (magic)
                {
                    inventoryObject.GetComponent<InventoryItems>().selected = objectType - 20;
                }
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
            cursorImage.sprite = cursorHand;
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
        cursorImage.sprite = cursorBasic;
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
            message.text = InventoryItems.redMushrooms.ToString() + " red mushrooms to be used";
        }
        if (objectType == 2)
        {
            message.text = InventoryItems.purpleMushrooms.ToString() + " purple mushrooms to be used";
        }
        if (objectType == 3)
        {
            message.text = InventoryItems.brownMushrooms.ToString() + " brown mushrooms to be used";
        }
        if (objectType == 4)
        {
            message.text = InventoryItems.blueFlowers.ToString() + " blue flowers to be used";
        }
        if (objectType == 5)
        {
            message.text = InventoryItems.redFlowers.ToString() + " red flowers to be used";
        }
        if (objectType == 6)
        {
            message.text = InventoryItems.roots.ToString()+" roots to be used";
        }
        if (objectType == 7)
        {
            message.text = InventoryItems.leafDews.ToString() + " leaf dews to be used";
        }
        if (objectType == 8)
        {
            message.text = "Special key to open chests";
        }
        if (objectType == 9)
        {
            message.text = InventoryItems.pinkEggs.ToString() + " pink eggs";
        }
        if (objectType == 10)
        {
            message.text = InventoryItems.bluePotions.ToString() + " blue potions";
        }
        if (objectType == 11)
        {
            message.text = InventoryItems.purplePotions.ToString() + " purple potions";
        }
        if (objectType == 12)
        {
            message.text = InventoryItems.greenPotions.ToString() + " green potions";
        }
        if (objectType == 13)
        {
            message.text = InventoryItems.redPotions.ToString() + " red potions";
        }
        if (objectType == 14)
        {
            message.text = InventoryItems.breads.ToString() + " breads to eat";
        }
        if (objectType == 15)
        {
            message.text = InventoryItems.cheese.ToString() + " cheese wheels to eat";
        }
        if (objectType == 16)
        {
            message.text = InventoryItems.meat.ToString() + " kg meat to eat";
        }
        //////// potions start here
        if (objectType == 20)
        {
            message.text = "Explosive fire attack";
        }
        if (objectType == 21)
        {
            message.text = "Replenishes full health";
        }
        if (objectType == 22)
        {
            message.text = "Become invisible for as long as mana lasts";
        }
        if (objectType == 23)
        {
            message.text = "Become invulnerable for as long as mana lasts";
        }
        if (objectType == 24)
        {
            message.text = "Double strength for as long as mana lasts";
        }
        if (objectType == 25)
        {
            message.text = "Swirl attack!";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        theCanvas.GetComponent<CreatePotion>().thisValue = objectType;
        theCanvas.GetComponent<CreatePotion>().UpdateValues();
    }
}
