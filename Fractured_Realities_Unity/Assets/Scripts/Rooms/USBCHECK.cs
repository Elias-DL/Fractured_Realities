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
        
        //heeft USBKey vast?
        if (equippedItem == "USBR3") // naam van de item/scriptable object niet de prefab naam
        {
            //activate canvas
            guessingGameCanvas.SetActive(true);
        }

    }
}
