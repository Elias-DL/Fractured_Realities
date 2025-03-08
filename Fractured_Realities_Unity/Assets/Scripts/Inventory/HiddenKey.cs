using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HiddenKey : MonoBehaviour
{
    public GameObject Player;
    public GameObject GlowKey;
    public GameObject MainCamera;
    public MeshRenderer GlowKleur;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        GlowKey = GameObject.FindWithTag("GlowKey");

        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Room1")
        {
            GlowKleur = GlowKey.GetComponent<MeshRenderer>();
        }
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        GlowKey = GameObject.FindWithTag("GlowKey");

        if (currentScene.name == "Room1")
        {
            MainCamera = GameObject.FindWithTag("MainCamera");
            string equippedItem = EquippedItemManager.Instance.EquippedItemName;
            int cameraLayer = LayerMask.NameToLayer("Camera");
            int defaultLayer = LayerMask.NameToLayer("Default");


            if (equippedItem == "Candle")
            {
                GlowKleur.enabled = true;


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
                GlowKleur.enabled = false;
                //Debug.Log("Pak de kaars oppp");
                MainCamera.layer = defaultLayer;

            }

        }

    }
}
