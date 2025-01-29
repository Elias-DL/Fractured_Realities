using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // For the new Unity Input System

public class DoorInteraction : MonoBehaviour
{
    
    [SerializeField] private string neccescaryKey;
    public string targetScene;          // Naam van de nieuwe scene
    public string targetSpawnPointName; // Naam van het teleportatiedoel in de nieuwe scene
    public GameObject Player;
    public GameObject ItemCanvas;
    public GameObject InventoryContent;
    public GameObject Inventory;
    public GameObject Managers;



    public void Start()
    {
        Player = GameObject.FindWithTag("Player");
        ItemCanvas = GameObject.FindWithTag("InventoryCanvas");
        InventoryContent = GameObject.FindWithTag("InventoryContent");
        Inventory = GameObject.FindWithTag("Inventory");
        Managers = GameObject.FindWithTag("Managers");
    }

    public void OnMouseDown()
    {
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

                //Instantiate(Player); //hierdoor spawn je wel juiste plek maar irritant met inventory enzo
                DontDestroyOnLoad(Player); //je blijft ook bestaan in og scene dus als je teruggaat zijn er 2 player
                DontDestroyOnLoad(ItemCanvas);
                DontDestroyOnLoad(InventoryContent);
                DontDestroyOnLoad(Inventory);
                DontDestroyOnLoad(Managers);

                Player.SetActive(true);
                ItemCanvas.SetActive(true);
                InventoryContent.SetActive(true);
                Inventory.SetActive(true);
                Managers.SetActive(true);


                
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
}