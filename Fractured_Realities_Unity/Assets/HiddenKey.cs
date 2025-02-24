using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HiddenKey : MonoBehaviour
{
    public GameObject Player;
    public GameObject GlowKey;
    public GameObject MainCamera;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        GlowKey = GameObject.FindWithTag("GlowKey");
       
        GlowKey.SetActive(false);
    }

    void Update()
    {

        MainCamera = GameObject.FindWithTag("MainCamera");
        string equippedItem = EquippedItemManager.Instance.EquippedItemName;
        int cameraLayer = LayerMask.NameToLayer("Camera");
        int defaultLayer = LayerMask.NameToLayer("Default");

        if (equippedItem == "Candle")
        {
            GlowKey.SetActive(true);

            // Change layer to "Camera"
            

            if (cameraLayer != -1)
            {
                MainCamera.layer = cameraLayer;
                Debug.Log("GlowKey layer changed to 'Camera'");
            }
            else
            {
                Debug.LogError("Layer 'Camera' does not exist. Please create it in Unity Layer settings.");
            }
        }
        else
        {
            GlowKey.SetActive(false);
            Debug.Log("Pak de kaars oppp");
            MainCamera.layer = defaultLayer;

        }
    }
}
