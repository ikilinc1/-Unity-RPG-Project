using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerMove.canMove = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerMove.canMove = true;
    }
}
