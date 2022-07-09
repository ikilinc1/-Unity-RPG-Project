using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayArmor : MonoBehaviour
{
    public GameObject[] armorTorso;
    public GameObject[] armorLegs;

    public void UpdateArmor()
    {
        for (int i = 0; i < armorTorso.Length; i++)
        {
            armorTorso[i].SetActive(false);
        }
        armorTorso[SaveScript.armor].SetActive(true);
        for (int i = 0; i < armorLegs.Length; i++)
        {
            armorLegs[i].SetActive(false);
        }
        armorLegs[SaveScript.armor].SetActive(true);

    }
}
