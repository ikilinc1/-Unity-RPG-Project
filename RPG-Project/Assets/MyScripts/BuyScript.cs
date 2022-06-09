using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyScript : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        max = itemAmountText.Length;
        currencyText.text = InventoryItems.gold.ToString();
    }
    
    public void CloseShop()
    {
        shopUI.SetActive(false);
    }

    public void BuyButton()
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

                    if (InventoryItems.gold >= cost[i])
                    {
                        if (inventoryItems[i] == 0)
                        {
                            InventoryItems.newIcon = iconNum[i];
                            InventoryItems.iconUpdate = true;
                        }
                        InventoryItems.gold -= cost[i];
                        if (tavern)
                        {
                            setTavernAmount(i);
                        }
                    }
                }
            }
        }
    }

    void UpdateTavernAmount()
    {
        inventoryItems[0] = InventoryItems.breads;
        inventoryItems[1] = InventoryItems.cheese;
        inventoryItems[2] = InventoryItems.meat;
    }
    
    void setTavernAmount(int item)
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
}
