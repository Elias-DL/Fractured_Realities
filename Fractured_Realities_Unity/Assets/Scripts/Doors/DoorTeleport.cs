using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // For the new Unity Input System

public class DoorInteraction : MonoBehaviour
{

    [SerializeField] private string neccescaryKey;
    public string targetScene;          // Naam van de nieuwe scene
    public string targetSpawnPointName; // Naam van het teleportatiedoel in de nieuwe scene
    public AudioSource src;
    public AudioClip sfx1;
    public void Start()
    {
        if (src == null)
        {
            src = GetComponent<AudioSource>();

        }
    }

    public void LoadScene()// nodig voor juiste teleportatie
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject inventoryUI = GameObject.FindGameObjectWithTag("Player");

        GameObject managers = GameObject.FindGameObjectWithTag("Managers");
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        //GameObject menuUI = GameObject.FindGameObjectWithTag("MenuUI");
        GameObject healthUI = GameObject.FindGameObjectWithTag("HealthUI");
        //GameObject hideInventory = GameObject.FindGameObjectWithTag("HideInventory");
        //GameObject showInventory = GameObject.FindGameObjectWithTag("ShowInventory");
        GameObject toggleInventory = GameObject.FindGameObjectWithTag("ToggleInventory");



        DontDestroyOnLoad(managers);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(player);


        player.SetActive(true);
        managers.SetActive(true);
        canvas.SetActive(true);
        //showInventory.SetActive(true);
        inventoryUI.SetActive(true);
        //healthUI.SetActive(true);
        toggleInventory.SetActive(true);

        //menuUI.SetActive(false);
        //hideInventory.SetActive(false);
    }

    public void OnMouseDown()
    {

        SoundEffects();
        //Check if the left mouse button was clicked
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            string equippedItem = EquippedItemManager.Instance.EquippedItemName;
            if (equippedItem == neccescaryKey) // in inspector bv : Key4, volledig niet alleen getal
            {
                //Stel de spawnpointnaam in voordat de scene wordt geladen
                SpawnManager.spawnPointName = targetSpawnPointName;

                //Laad de nieuwe scene
                Debug.Log("travel to " + targetScene + " door " + targetSpawnPointName);
                SceneManager.LoadScene(targetScene);

                LoadScene();



            }
            else
            {
                if (equippedItem == null)
                {
                    Debug.Log(" - TIP : " + neccescaryKey);

                }
                else
                {
                    Debug.Log("You don't have the necessary key equipped! , you have " + equippedItem + " and you need key " + neccescaryKey);

                }
            }

        }
    }

    public void SoundEffects()
    {
        src.clip = sfx1;
        src.volume = 1;
        src.Play();


        //if (src != null) NIET NODIG DENK Ik
        //{
        //    if (string.IsNullOrEmpty(action) || (action != "Walk" && src.isPlaying))
        //    {
        //        src.Stop(); // Stop the sound instantly when no action is detected
        //    }

        //}


    }
}
