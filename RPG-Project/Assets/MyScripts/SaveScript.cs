using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveScript : MonoBehaviour
{
    public static int pchar = 0;
    public static string pname = "player";
    public static GameObject spawnPoint;
    public static GameObject theTarget;
    public static float manaAmount = 1f;
    public static float staminaAmount = 1f;
    public static bool invisible = false;
    public static bool invulnerable = false;
    public static float strengthAmount = 0.1f;
    public static float manaPowerAmount = 0.1f;
    public static float staminaPowerAmount = 0.1f;
    public static float playerHealth = 1.0f;
    public static int killAmount = 0;
    public static int weaponChoice = 0;
    public static bool weaponChange = false;
    public static bool carryingWeapon = false;
    public static int armor = 0;
    public static bool changeArmor = false;
    public static float playerLevel = 0.1f;
    public static int weaponIncrease;
    public static int strengthIncrease = 0;
    public static float armorValue = 0f;
    public static int enemiesOnScreen;

    public static bool saving = false;
    public static bool continueData = false;
    private bool checkForLoad = false;
    private GameObject inventoryObj;
    
    // save data
    public int pcharS;
    public string pnameS;
    public float strengthAmtS;
    public float manaPowerAmtS;
    public float staminaPowerAmtS;
    public int killAmtS;
    public int weaponChoiceS;
    public bool carryingWeaponS;
    public int armorS;
    public float playerLevelS;
    public int weaponIncreaseS;
    public float playerHealthS;
    public int strengthIncreaseS;
    public float armorValueS;
    public int goldS;
    public bool keyS;
    public int redMushroomsS;
    public int purpleMushroomsS;
    public int brownMushroomsS;
    public int blueFlowersS;
    public int redFlowersS;
    public int rootsS;
    public int leafDewS;
    public int dragonEggS;
    public int redPotionS;
    public int bluePotionS;
    public int greenPotionS;
    public int purplePotionS;
    public int breadS;
    public int cheeseS;
    public int meatS;
    public bool magicCollectedS;
    public bool spellsCollectedS;
    public bool[] weaponS;
    public int[] objectTypeS;
    
    private int checkAmount = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        if (continueData)
        {
            string fileLocation = Application.persistentDataPath + "/save.dat";
            StreamReader reader = new StreamReader(fileLocation);
            string saveData = reader.ReadToEnd();
            reader.Close();
            JsonUtility.FromJsonOverwrite(saveData, this);

            pchar = pcharS;
            continueData = false;
            checkForLoad = true;
        }
    }

    private void Update()
    {
        if (manaAmount < 1f)
        {
            manaAmount += (manaPowerAmount/10 +  0.04f) * Time.deltaTime;
        }
        if (manaAmount <= 0)
        {
            manaAmount = 0;
            invisible = false;
        }
        if (manaAmount < 0.03f)
        {
            invisible = false;
            invulnerable = false;
            strengthIncrease = 0;
        }
        
        if (staminaAmount < 1f)
        {
            staminaAmount += (staminaPowerAmount/10 + 0.04f) * Time.deltaTime;
        }
        if (staminaAmount <= 0)
        {
            staminaAmount = 0;
        }

        if (killAmount == checkAmount)
        {
            playerLevel += 0.1f;
            checkAmount = killAmount + 10;
            strengthAmount = playerLevel;
            manaPowerAmount = playerLevel;
            staminaPowerAmount = playerLevel;
            weaponIncrease = System.Convert.ToInt32(strengthAmount * 90);
        }

        if (armor == 1)
        {
            armorValue = 0.002f;
        }
        if (armor == 2)
        {
            armorValue = 0.004f;
        }

        if (saving)
        {
            saving = false;
            if (inventoryObj == null)
            {
                inventoryObj = GameObject.Find("InventoryCanvas");
            }
            pcharS = pchar;
            pnameS = pname;
            strengthAmtS = strengthAmount;
            manaPowerAmtS = manaPowerAmount;
            staminaPowerAmtS = staminaPowerAmount;
            killAmtS = killAmount;
            weaponChoiceS = weaponChoice;
            carryingWeaponS = carryingWeapon;
            armorS = armor;
            playerLevelS = playerLevel;
            weaponIncreaseS = weaponIncrease;
            playerHealthS = playerHealth;
            strengthIncreaseS = strengthIncrease;
            armorValueS = armorValue;
            goldS = InventoryItems.gold;
            keyS = InventoryItems.key;
            redMushroomsS = InventoryItems.redMushrooms;
            purpleMushroomsS = InventoryItems.purpleMushrooms;
            brownMushroomsS = InventoryItems.brownMushrooms;
            blueFlowersS = InventoryItems.blueFlowers;
            redFlowersS = InventoryItems.redFlowers;
            rootsS = InventoryItems.roots;
            leafDewS = InventoryItems.leafDews;
            dragonEggS = InventoryItems.pinkEggs;
            redPotionS = InventoryItems.redPotions;
            bluePotionS = InventoryItems.bluePotions;
            greenPotionS = InventoryItems.greenPotions;
            purplePotionS = InventoryItems.purplePotions;
            breadS = InventoryItems.breads;
            cheeseS = InventoryItems.cheese;
            meatS = InventoryItems.meat;
            magicCollectedS = BookCollect.magicCollected;
            spellsCollectedS = BookCollect.spellsCollected;
            weaponS = inventoryObj.GetComponent<InventoryItems>().weapons;
            for (int i = 0; i < 16; i++)
            {
                objectTypeS[i] = inventoryObj.GetComponent<InventoryItems>().emptySlots[i].transform.gameObject.GetComponent<HintMessage>().objectType;
            }

            string saveData = JsonUtility.ToJson(this);
            string fileLocation = Application.persistentDataPath + "/save.dat";
            StreamWriter writer = new StreamWriter(fileLocation);
            writer.WriteLine(saveData);
            writer.Close();
        }

        if (checkForLoad)
        {
            int sceneNumber = SceneManager.GetActiveScene().buildIndex;
            if (sceneNumber == 2)
            {
                if (inventoryObj == null)
                {
                    inventoryObj = GameObject.Find("InventoryCanvas");
                }

                if (inventoryObj != null)
                {
                    pname = pnameS;
                    strengthAmount = strengthAmtS;
                    manaPowerAmount = manaPowerAmtS;
                    staminaPowerAmount = staminaPowerAmtS;
                    killAmtS = killAmount;
                    weaponChoice = weaponChoiceS;
                    carryingWeapon = carryingWeaponS;
                    armor = armorS;
                    playerLevel = playerLevelS;
                    weaponIncrease = weaponIncreaseS;
                    playerHealth = playerHealthS;
                    strengthIncrease = strengthIncreaseS;
                    armorValue = armorValueS;
                    InventoryItems.gold = goldS;
                    InventoryItems.key = keyS;
                    InventoryItems.redMushrooms = redMushroomsS;
                    InventoryItems.purpleMushrooms = purpleMushroomsS;
                    InventoryItems.brownMushrooms = brownMushroomsS;
                    InventoryItems.blueFlowers = blueFlowersS;
                    InventoryItems.redFlowers = redFlowersS;
                    InventoryItems.roots = rootsS;
                    InventoryItems.leafDews = leafDewS;
                    InventoryItems.pinkEggs = dragonEggS;
                    InventoryItems.redPotions = redPotionS;
                    InventoryItems.bluePotions = bluePotionS;
                    InventoryItems.greenPotions = greenPotionS;
                    InventoryItems.purplePotions = purplePotionS;
                    InventoryItems.breads = breadS;
                    InventoryItems.cheese = cheeseS;
                    InventoryItems.meat = meatS;
                    BookCollect.magicCollected = magicCollectedS;
                    BookCollect.spellsCollected = spellsCollectedS;

                    if (magicCollectedS)
                    {
                        inventoryObj.GetComponent<InventoryItems>().magicUI.SetActive(true);
                    }
                    
                    if (spellsCollectedS)
                    {
                        inventoryObj.GetComponent<InventoryItems>().spellsUI.SetActive(true);
                    }
                    
                    inventoryObj.GetComponent<InventoryItems>().weapons = weaponS;
                    if (carryingWeapon)
                    {
                        weaponChange = true;
                    }

                    if (armor > 0)
                    {
                        changeArmor = true;
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        inventoryObj.GetComponent<InventoryItems>().emptySlots[i].sprite =
                            inventoryObj.GetComponent<InventoryItems>().icons[objectTypeS[i]];
                         inventoryObj.GetComponent<InventoryItems>().emptySlots[i].transform.gameObject.GetComponent<HintMessage>().objectType = objectTypeS[i];
                    }

                    checkForLoad = false;
                }
            }
        }
    }
}
