using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HiddenKey : MonoBehaviour
{
    public GameObject player;
    public GameObject glowKey;
    public GameObject mainCamera;
    public MeshRenderer glowKleur;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        glowKey = GameObject.FindWithTag("GlowKey");

        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Room1")
        {
            glowKleur = glowKey.GetComponent<MeshRenderer>();
        }
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        glowKey = GameObject.FindWithTag("GlowKey");

        if (currentScene.name == "Room1")
        {
            mainCamera = GameObject.FindWithTag("MainCamera");
            string equippedItem = EquippedItemManager.Instance.EquippedItemName;
            int cameraLayer = LayerMask.NameToLayer("Camera");
            int defaultLayer = LayerMask.NameToLayer("Default");


            if (equippedItem == "Candle")
            {
                glowKleur.enabled = true;


                if (cameraLayer != -1)
                {
                    mainCamera.layer = cameraLayer; // Het blauwe effect wordt enkel op een ander layer getoond van de camera, hierdoor moet je deze eerst switchen
                    Debug.Log("GlowKey layer ->  'Camera'");
                }
                else
                {
                    Debug.LogError("Layer 'Camera' bestaat niet");
                }
            }
            else
            {
                glowKleur.enabled = false;
                //Debug.Log("Pak de kaars op");
                mainCamera.layer = defaultLayer;

            }

        }

    }
}
