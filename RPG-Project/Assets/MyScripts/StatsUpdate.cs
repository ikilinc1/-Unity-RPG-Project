using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpdate : MonoBehaviour
{
    public Text nameText;
    public Text currencyText;
    public Text killAmountText;

    public Image strengthBar;
    public Image manaBar;
    public Image staminaBar;

    public GameObject[] weaponButtons;
    public GameObject inventoryObj;
    public bool updateWeapons = true;
    public GameObject[] items;
    
    /*Custom*/
    private bool setBlacksmith = true;
    
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = SaveScript.pname;
        if (SaveScript.pchar == 1 || SaveScript.pchar == 5 || SaveScript.pchar == 6 || SaveScript.pchar == 7)
        {
            items[0].SetActive(true);
            items[1].SetActive(false);
        }
        else
        {
            items[1].SetActive(true);
            items[0].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currencyText.text = InventoryItems.gold.ToString();
        killAmountText.text = SaveScript.killAmount.ToString();
        strengthBar.fillAmount = SaveScript.strengthAmount;
        manaBar.fillAmount = SaveScript.manaPowerAmount;
        staminaBar.fillAmount = SaveScript.staminaPowerAmount;

        if (setBlacksmith)
        {
            setBlacksmith = false;
            if (SaveScript.pchar == 1 || SaveScript.pchar == 5 || SaveScript.pchar == 6 || SaveScript.pchar == 7)
            {
                items[0].SetActive(true);
                items[1].SetActive(false);
            }
            else
            {
                items[1].SetActive(true);
                items[0].SetActive(false);
            }
        }
        
        if (updateWeapons)
        {
            for (int i = 0; i < weaponButtons.Length; i++)
            {
                if (inventoryObj.GetComponent<InventoryItems>().weapons[i])
                {
                    weaponButtons[i].SetActive(true);
                }
            }
        }

        if (isActiveAndEnabled)
        {
            updateWeapons = false;
        }
    }
}
