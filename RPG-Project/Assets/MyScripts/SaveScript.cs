using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    public static int pchar = 0;
    public static string pname = "player";
    public static GameObject spawnPoint;
    public static GameObject theTarget;
    public static float manaAmount = 1f;
    public static float staminaAmount = 1f;
    public static bool invisible = false;
    public static float strengthAmount = 0.1f;
    public static float manaPowerAmount = 0.1f;
    public static float staminaPowerAmount = 0.1f;
    public static int killAmount = 0;
    public static int weaponChoice = 0;
    public static bool weaponChange = false;
    public static bool carryingWeapon = false;
    public static int armor = 0;
    public static bool changeArmor = false;
    public static float playerLevel = 0.1f;
    public static int weaponIncrease;
    
    private int checkAmount = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
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
            checkAmount = killAmount + 2;
            strengthAmount = playerLevel;
            manaPowerAmount = playerLevel;
            staminaPowerAmount = playerLevel;
            weaponIncrease = System.Convert.ToInt32(strengthAmount * 90);
        }
    }
}
