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
        if (equippedItem == "USBKey")
        {
            //activate canvas
            guessingGameCanvas.SetActive(true);
        }

    }
}
