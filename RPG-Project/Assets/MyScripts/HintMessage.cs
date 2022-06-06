using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HintMessage : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hintBox;
    public Text message;

    private Vector3 screenPoint;
    // Start is called before the first frame update
    void Start()
    {
        hintBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hintBox.SetActive(true);
        screenPoint.x = Input.mousePosition.x + 320;
        screenPoint.y = Input.mousePosition.y + 150;
        screenPoint.z = 1f;
        hintBox.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        MessageDisplay();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hintBox.SetActive(false);
    }

    void MessageDisplay()
    {
        message.text = "empty";
    }
}
