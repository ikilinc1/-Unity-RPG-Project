using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePotion : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        expectedValue = values[0];
        max = emptySlots.Length;
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
                    value = 0;
                    thisValue = 0;
                }
            }
            max = emptySlots.Length;
        }
    }

    public void UpdateValues()
    {
        value += thisValue;
        expectedValue = values[itemId];
    }
}
