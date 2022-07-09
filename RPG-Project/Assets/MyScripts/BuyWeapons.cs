using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyWeapons : MonoBehaviour
{
    public int weaponNumber;
    public int cost;
    public Text currencyText;
    public GameObject inventoryObj;
    private AudioSource audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        currencyText.text = InventoryItems.gold.ToString();
        audioPlayer = inventoryObj.GetComponent<AudioSource>();
    }

    public void BuyWeaponButton()
    {
        if (InventoryItems.gold >= cost)
        {
            InventoryItems.gold -= cost;
            inventoryObj.GetComponent<InventoryItems>().weapons[weaponNumber] = true;
            audioPlayer.clip = inventoryObj.GetComponent<InventoryItems>().buySound;
            audioPlayer.Play();
            currencyText.text = InventoryItems.gold.ToString();
        }
    }
}
