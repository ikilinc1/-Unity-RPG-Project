using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePotion : MonoBehaviour
{
    public GameObject inventoryObject;
    private AudioSource audioPlayer;
    
    public int[] values;
    [HideInInspector]
    public int expectedValue;
    [HideInInspector]
    public int value;

    public Image[] emptySlots;
    public Sprite[] icons;
    public Sprite emptyIcon;

    [HideInInspector]
    public int itemId = 0;

    [HideInInspector]
    public int thisValue;
    private int max;

    private int maxTwo;
    
    // Start is called before the first frame update
    void Start()
    {
        expectedValue = values[0];
        max = emptySlots.Length;
        maxTwo = emptySlots.Length;
        audioPlayer = inventoryObject.GetComponent<AudioSource>();
        Create(); // added because of a bug
    }

    public void Create()
    {
        if (expectedValue == value)
        {
            for (int i = 0; i < max; i++)
            {
                if (emptySlots[i].sprite == emptyIcon)
                {
                    max = i;
                    emptySlots[i].sprite = icons[itemId];
                    emptySlots[i].transform.gameObject.GetComponent<HintMessage>().objectType = itemId + 20;
                    audioPlayer.clip = inventoryObject.GetComponent<InventoryItems>().createPotionSound;
                    audioPlayer.Play();
                    value = 0;
                    thisValue = 0;
                }
            }
            max = emptySlots.Length;
        }
    }

    public void Remove(int index)
    {
        for (int i = 0; i < maxTwo; i++)
        {
            if (emptySlots[i].sprite == icons[index])
            {
                maxTwo = i;
                emptySlots[i].sprite = emptyIcon;
                emptySlots[i].transform.gameObject.GetComponent<HintMessage>().objectType = 0;
            }
        }

        maxTwo = emptySlots.Length;
    }

    public void UpdateValues()
    {
        value += thisValue;
        expectedValue = values[itemId];
    }
}
