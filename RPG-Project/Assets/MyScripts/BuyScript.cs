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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
    }
}
