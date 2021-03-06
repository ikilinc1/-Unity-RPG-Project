using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyScript : MonoBehaviour
{
    public GameObject inventoryObject;
    private AudioSource audioPlayer;
    
    public GameObject shopUI;
    public Text currencyText;
    public bool tavern = false;

    public int[] amount;
    public int[] cost;
    public int[] iconNum;
    public int[] inventoryItems;
    public Text[] itemAmountText;
    
    private Text compare;
    private int max = 0;
    private bool canClick = true;

    // Start is called before the first frame update
    void Start()
    {
        max = itemAmountText.Length;
        currencyText.text = InventoryItems.gold.ToString();
        audioPlayer = inventoryObject.GetComponent<AudioSource>();
    }
    
    public void CloseShop()
    {
        shopUI.SetActive(false);
    }

    public void BuyButton()
    {
        if (canClick)
        {
            for (int i = 0; i < max; i++)
            {
                if (itemAmountText[i] == compare)
                {
                    max = i;
                    if (amount[i] > 0)
                    {
                        if (tavern)
                        {
                            UpdateTavernAmount();
                        }
                        else
                        {
                            UpdateWizardAmount();
                        }
                        if (InventoryItems.gold >= cost[i])
                        {
                            if (inventoryItems[i] == 0)
                            {
                                InventoryItems.newIcon = iconNum[i];
                                InventoryItems.iconUpdate = true;
                            }
                            InventoryItems.gold -= cost[i];
                            audioPlayer.clip = inventoryObject.GetComponent<InventoryItems>().buySound;
                            audioPlayer.Play();
                            if (tavern)
                            {
                                SetTavernAmount(i);
                            }
                            else
                            {
                                SetWizardAmount(i);
                            }
                        }
                    }
                }
            }
        }
        
    }

    public void UpdateGold()
    {
        currencyText.text = InventoryItems.gold.ToString();
    }
    
    void UpdateTavernAmount()
    {
        inventoryItems[0] = InventoryItems.breads;
        inventoryItems[1] = InventoryItems.cheese;
        inventoryItems[2] = InventoryItems.meat;
    }
    
    void UpdateWizardAmount()
    {
        inventoryItems[0] = InventoryItems.redPotions;
        inventoryItems[1] = InventoryItems.purplePotions;
        inventoryItems[2] = InventoryItems.bluePotions;
        inventoryItems[3] = InventoryItems.greenPotions;
        inventoryItems[4] = InventoryItems.pinkEggs;
        inventoryItems[5] = InventoryItems.roots;
        inventoryItems[6] = InventoryItems.leafDews;
    }
    
    void SetTavernAmount(int item)
    {
        if (item == 0)
        {
            InventoryItems.breads++;
        }
        if (item == 1)
        {
            InventoryItems.cheese++;
        }
        if (item == 2)
        {
            InventoryItems.meat++;
        }

        amount[item]--;
        itemAmountText[item].text = amount[item].ToString();
        currencyText.text = InventoryItems.gold.ToString();
        max = itemAmountText.Length;
    }

    void SetWizardAmount(int item)
    {
        if (item == 0)
        {
            InventoryItems.redPotions++;
        }
        if (item == 1)
        {
            InventoryItems.purplePotions++;
        }
        if (item == 2)
        {
            InventoryItems.bluePotions++;
        }
        if (item == 3)
        {
            InventoryItems.greenPotions++;
        }
        if (item == 4)
        {
            InventoryItems.pinkEggs++;
        }
        if (item == 5)
        {
            InventoryItems.roots++;
        }
        if (item == 6)
        {
            InventoryItems.leafDews++;
        }

        amount[item]--;
        itemAmountText[item].text = amount[item].ToString();
        currencyText.text = InventoryItems.gold.ToString();
        max = itemAmountText.Length;
    }
    
    public void Bread()
    {
        compare = itemAmountText[0];
        Check(0);
    }
    public void Cheese()
    {
        compare = itemAmountText[1];
        Check(1);
    }
    public void Meat()
    {
        compare = itemAmountText[2];
        Check(2);
    }
    public void RedPotion()
    {
        compare = itemAmountText[0];
        Check2(0);
    }
    public void PurplePotion()
    {
        compare = itemAmountText[1];
        Check2(1);
    }
    public void BluePotion()
    {
        compare = itemAmountText[2];
        Check2(2);
    }
    public void GreenPotion()
    {
        compare = itemAmountText[3];
        Check2(3);
    }
    public void PinkEgg()
    {
        compare = itemAmountText[4];
        Check2(4);
    }
    public void Root()
    {
        compare = itemAmountText[5];
        Check2(5);
    }
    public void LeafDew()
    {
        compare = itemAmountText[6];
        Check2(6);
    }

    void Check(int b)
    {
        if (amount[b] > 0)
        {
            canClick = true;
        }
        else
        {
            canClick = false;
        }
    }
    
    void Check2(int c)
    {
        if (amount[c] > 0)
        {
            canClick = true;
        }
        else
        {
            canClick = false;
        }
    }
}
