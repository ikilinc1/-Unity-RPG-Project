using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItems : MonoBehaviour
{
    public GameObject inventoryMenu;
    public GameObject openBook;
    public GameObject closedBook;
    public GameObject messageBox;
    public GameObject potionBook;
    public GameObject theCanvas;

    public GameObject inventoryScreen;
    public GameObject statsScreen;
    public GameObject characterDisplay;

    private AudioSource audioPlayer;
    public AudioClip bookOpenSound;
    public AudioClip selectSound;
    public AudioClip buySound;
    public AudioClip createPotionSound;
    public AudioClip pickupSound;

    public Image[] emptySlots;
    public Sprite[] icons;
    public Sprite emptyIcon;

    public static int redMushrooms = 0;
    public static int purpleMushrooms = 0;
    public static int brownMushrooms = 0;
    public static int blueFlowers = 0;
    public static int redFlowers = 0;
    public static int roots = 0;
    public static int leafDews = 0;
    public static bool key = false;
    public static int pinkEggs = 0;
    public static int bluePotions = 0;
    public static int purplePotions = 0;
    public static int greenPotions = 0;
    public static int redPotions = 0;
    public static int breads = 0;
    public static int cheese = 0;
    public static int meat = 0;
    
    public static int newIcon = 0;
    public static int gold = 30000;
    public static bool iconUpdate = false;
    private int max;

    [HideInInspector]
    public string entry;
    public string[] items;
    [HideInInspector]
    public int currentId = 0;
    [HideInInspector]
    public int checkAmount = 0;
    private int maxTwo;
    private int maxThree;

    public Image[] UISlots;
    public Sprite[] magicIcons;
    public Sprite[] spellIcons;
    public KeyCode[] keys;
    public bool set = false;
    public bool setTwo = false;
    [HideInInspector] public int selected = 0;
    public int[] magicAttack;
    public AudioClip[] magicSounds;

    public GameObject[] magicParticles;
    public Image manaBar;
    public Image staminaBar;
    public Image healthBar;

    private GameObject playerObj;
    private Animator playerAnim;
    private float weightAmount = 1.0f;
    private bool changeWeight = false;
    private AnimatorStateInfo playerInfo;

    public bool[] weapons;

    // Start is called before the first frame update
    void Start()
    {
        inventoryMenu.SetActive(false);
        openBook.SetActive(false);
        closedBook.SetActive(true);
        potionBook.SetActive(false);
        max = emptySlots.Length;
        maxTwo = items.Length;
        maxThree = emptySlots.Length;
        audioPlayer = GetComponent<AudioSource>();
        //playerObj = GameObject.FindGameObjectWithTag("Player");
        //playerAnim = playerObj.GetComponent<Animator>();
        StartCoroutine(FindPlayer());
        //TEMP
        redMushrooms = 0; 
        purpleMushrooms = 0; 
        brownMushrooms = 0; 
        blueFlowers = 0; 
        redFlowers = 0;
        roots = 0;
        leafDews = 0;
        pinkEggs = 0;
        bluePotions = 0;
        purplePotions = 0;
        greenPotions = 0;
        redPotions = 0;
        breads = 0;
        cheese = 0;
        meat = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnim)
        {
            playerInfo = playerAnim.GetCurrentAnimatorStateInfo(1);
        }

        healthBar.fillAmount = SaveScript.playerHealth;
        
        if (iconUpdate)
        {
            for (int i = 0; i < max; i++)
            {
                if (emptySlots[i].sprite == emptyIcon)
                {
                    max = i;
                    emptySlots[i].sprite = icons[newIcon];
                    emptySlots[i].transform.gameObject.GetComponent<HintMessage>().objectType = newIcon;
                }
            }

            StartCoroutine(Reset());
        }
        
        if (set)
        {
            for (int i = 0; i < UISlots.Length; i++)
            {
                if (Input.GetKeyDown(keys[i]))
                {
                    set = false;
                    UISlots[i].sprite = magicIcons[selected];
                    magicAttack[i] = selected;
                    theCanvas.GetComponent<CreatePotion>().Remove(selected);
                }
            }
        }
        if (setTwo)
        {
            for (int i = 0; i < UISlots.Length; i++)
            {
                if (Input.GetKeyDown(keys[i]))
                {
                    setTwo = false;
                    UISlots[i].sprite = spellIcons[selected];
                    magicAttack[i] = selected += 6;
                }
            }
        }

        if (Input.anyKey && Time.timeScale == 1)
        {
            for (int i = 0; i < UISlots.Length; i++)
            {
                if (Input.GetKeyDown(keys[i]))
                {
                    if (UISlots[i].sprite != emptyIcon)
                    {
                        if (SaveScript.manaAmount > 0.1f)
                        {
                            Instantiate(magicParticles[magicAttack[i]], SaveScript.spawnPoint.transform.position, SaveScript.spawnPoint.transform.rotation);
                            audioPlayer.clip = magicSounds[magicAttack[i]];
                            audioPlayer.Play();
                            playerAnim.SetTrigger("magicAttack");
                            playerAnim.SetLayerWeight(1, 1);
                            weightAmount = 1;
                        }

                        if (magicAttack[i] < 6 && SaveScript.manaAmount > 0.1f)
                        {
                            UISlots[i].sprite = emptyIcon;
                        }
                    }
                }
            }
        }

        manaBar.fillAmount = SaveScript.manaAmount;

        if (SaveScript.staminaAmount != staminaBar.fillAmount)
        {
            staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, SaveScript.staminaAmount, 2 * Time.deltaTime);
        }

        if (playerInfo.IsTag("magic"))
        {
            changeWeight = true;
        }
        
        if (changeWeight == true)
        {
            weightAmount -= 0.4f * Time.deltaTime;
            playerAnim.SetLayerWeight(1,weightAmount);
            if (weightAmount <= 0)
            {
                changeWeight = false;
            }
        }

        if (breads == 0)
        {
            RemoveIcon(14);
        }
        if (cheese == 0)
        {
            RemoveIcon(15);
        }
        if (meat == 0)
        {
            RemoveIcon(16);
        }
    }

    public void CheckStatics()
    {
        for (int i = 0; i < maxTwo; i++)
        {
            if (i == currentId)
            {
                maxTwo = i;
                entry = items[i];
                checkAmount = System.Convert.ToInt32(typeof(InventoryItems).GetField(entry).GetValue(null));
                checkAmount--;
                typeof(InventoryItems).GetField(entry).SetValue(null, checkAmount);
                if (checkAmount == 0)
                {
                    RemoveIcon(i);
                }
            }
        }
        maxTwo = items.Length;
    }

    public void RemoveIcon(int iconType)
    {
        for (int i = 0; i < maxThree; i++)
        {
            if (emptySlots[i].sprite == icons[iconType])
            {
                maxThree = i;
                emptySlots[i].sprite = icons[0];
                emptySlots[i].transform.gameObject.GetComponent<HintMessage>().objectType = 0;
            }
        }
        maxThree = emptySlots.Length;
    }

    public void OpenMenu()
    {
        messageBox.SetActive(false);
        inventoryMenu.SetActive(true);
        openBook.SetActive(true);
        closedBook.SetActive(false);
        audioPlayer.clip = bookOpenSound;
        audioPlayer.Play();
        SaveScript.theTarget = null;
        OpenInventoryScreen();
        Time.timeScale = 0;
    }
    
    public void CloseMenu()
    {
        inventoryMenu.SetActive(false);
        openBook.SetActive(false);
        closedBook.SetActive(true);
        audioPlayer.clip = bookOpenSound;
        audioPlayer.Play();
        characterDisplay.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenInventoryScreen()
    {
        statsScreen.SetActive(false);
        characterDisplay.SetActive(false);
        inventoryScreen.SetActive(true);
    }
    
    public void OpenStatsScreen()
    {
        inventoryScreen.SetActive(false);
        statsScreen.SetActive(true);
        characterDisplay.SetActive(true);
        statsScreen.GetComponent<StatsUpdate>().updateWeapons = true;
        characterDisplay.GetComponent<PlayerDisplay>().ChangeDisplayArmor();
    }

    public void OpenPotionBook()
    {
        potionBook.SetActive(true);
    }
    
    public void ClosePotionBook()
    {
        theCanvas.GetComponent<CreatePotion>().thisValue = 0;
        theCanvas.GetComponent<CreatePotion>().value = 0;
        potionBook.SetActive(false);
    }
    
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.1f);
        iconUpdate = false;
        max = emptySlots.Length;
    }
    
    IEnumerator FindPlayer()
    {

        yield return new WaitForSeconds(0.5f);

        playerObj = GameObject.FindGameObjectWithTag("Player");

        playerAnim = playerObj.GetComponent<Animator>();

    }
}
