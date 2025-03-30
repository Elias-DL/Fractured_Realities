using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBCHECK : MonoBehaviour
{
    public GameObject guessingGameCanvas;

    

    //zodra klik
    void OnMouseDown()
    {
        //check wat vast
        string equippedItem = EquippedItemManager.Instance.EquippedItemName;
        
        //heeft USBKey vast?
        if (equippedItem == "USBR3") // naam van de item/scriptable object niet de prefab naam
        {
            //activate canvas
            guessingGameCanvas.SetActive(true);
        }

    }
}
