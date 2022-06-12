using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBook : MonoBehaviour
{
    public Image magicIcon;
    public Text magicName;
    public Text magicDescription;

    public Sprite[] magicSprites;
    public string[] names;
    public string[] descriptions;

    public GameObject[] iconSets;

    private int currentIcon = 0;
    
    public GameObject theCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        magicIcon.sprite = magicSprites[0];
        magicName.text = names[0];
        magicDescription.text = descriptions[0];
        iconSets[0].SetActive(true);
    }

    public void Next()
    {
        if (currentIcon < magicSprites.Length - 1)
        {
            currentIcon++;
            magicIcon.sprite = magicSprites[currentIcon];
            magicName.text = names[currentIcon];
            magicDescription.text = descriptions[currentIcon];
            SwitchOff();
            iconSets[currentIcon].SetActive(true);
            theCanvas.GetComponent<CreatePotion>().itemId++;
            theCanvas.GetComponent<CreatePotion>().value = 0;
            theCanvas.GetComponent<CreatePotion>().thisValue = 0;
        }
    }
    
    public void Back()
    {
        if (currentIcon > 0)
        {
            currentIcon--;
            magicIcon.sprite = magicSprites[currentIcon];
            magicName.text = names[currentIcon];
            magicDescription.text = descriptions[currentIcon];
            SwitchOff();
            iconSets[currentIcon].SetActive(true);
            theCanvas.GetComponent<CreatePotion>().itemId--;
            theCanvas.GetComponent<CreatePotion>().value = 0;
            theCanvas.GetComponent<CreatePotion>().thisValue = 0;
        }
    }

    void SwitchOff()
    {
        for (int i = 0; i < iconSets.Length; i++)
        {
            iconSets[i].SetActive(false);
        }
    }
}
