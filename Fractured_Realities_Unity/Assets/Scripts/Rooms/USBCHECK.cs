using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBCHECK : MonoBehaviour
{
    public GameObject guessingGameCanvas;
    public GameObject canvas;
    

    //zodra klik
    void OnMouseDown()
    {

        canvas = GameObject.FindWithTag("Canvas");
        //check wat vast
        string equippedItem = EquippedItemManager.Instance.EquippedItemName;
        guessingGameCanvas = canvas.GetComponent<MenuLogic>().guessingGameUI;
        //Debug.Log(EquippedItemManager.Instance.EquippedItemName);
        //heeft USBKey vast?
        if (equippedItem == "USB") // naam van de item/scriptable object niet de prefab naam
        {
            //activate canvas
            guessingGameCanvas.SetActive(true);
        }

    }
}
