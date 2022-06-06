using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Time.timeScale == 1)
        {
            PlayerMove.canMove = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Time.timeScale == 1)
        {
            PlayerMove.canMove = true;
        }
    }
}
