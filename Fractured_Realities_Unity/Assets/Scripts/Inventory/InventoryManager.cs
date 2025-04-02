using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<Item> Items = new List<Item>();
    public Transform PlayerTransform; // in script aangemaakt
    public GameObject itemPrefab; // in inspector 
    public Transform ItemContent; // in script aangemaakt

    public GameObject InventoryItem;

    public InventoryItemController[] InventoryItems;

    public GameObject Player;
    public GameObject InventoryContent;
    public GameObject Inventory;


    public GameObject HideInventory;
    public GameObject ShowInventory;


    Scene currentScene;
    string currentSceneName;

    int InventoryActiveORNot = 0;

    public void Awake()
    {

        Instance = this;

    }

    public void Start()
    {

        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
        //Debug.Log("start inventorymanager " + currentSceneName);
        
        HideInventory = GameObject.FindWithTag("HideInventory");
        ShowInventory = GameObject.FindWithTag("ShowInventory");
    }
    public void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;

        if (currentSceneName != "Main Menu")
        {
            //Player = GameObject.FindWithTag("Player");
            //PlayerTransform = Player.transform;
            //Inventory = GameObject.FindWithTag("Inventory");

            if (InventoryActiveORNot %2 != 0)
            {
                //InventoryContent = GameObject.FindWithTag("InventoryContent");
                ItemContent = InventoryContent.transform;

            }

            
        }


        //old Input system
        if (Input.GetKeyDown(KeyCode.E))  /// fornite : caps lock, Minecraf : E,  warzone : caps lock belangerijke keuze!!!!!!!!!!!!!
        {
            ToggleInventory();

        }
    }

    public void ToggleInventory()
    {

        // Toggle  inventorys visibility
        bool isActive = Inventory.activeSelf;
        Inventory.SetActive(!isActive);

        // UI Refresh als de inventory wordt geopend
        if (!isActive)
        {
            ListItems();
        }

        // toggle invenotory visibility check
        if (isActive) 
        {
           
            ShowInventory.SetActive(true);
            HideInventory.SetActive(false);
        }
        else
        {
            
            ShowInventory.SetActive(false);
            HideInventory.SetActive(true);
        }
    }


    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        // Remove the item from the inventory list
        Items.Remove(item);

        // Optionally refresh the inventory UI (if you want immediate feedback)
        ListItems();
    }

    public void ListItems()
    {
       // Debug.Log("ListItems called");

        // Clear out any existing inventory items in the UI
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        // Clear the InventoryItems array before setting new ones
        InventoryItems = new InventoryItemController[Items.Count];

        // Instantiate new inventory items
        for (int i = 0; i < Items.Count; i++)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            obj.SetActive(true); // Ensure the item is visible

            // Find the UI components inside the instantiated prefab
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();

            // Set the UI text and icon for the item
            itemName.text = Items[i].itemName;
            itemIcon.sprite = Items[i].icon;

            obj.name = itemName.text;

            // Assign the item to the InventoryItemController component
            InventoryItemController controller = obj.GetComponent<InventoryItemController>();

            // Ensure the correct item and prefab are assigned
            controller.AddItem(Items[i]);

            // Assign to the InventoryItems array
            InventoryItems[i] = controller;

            // Add the click event to equip the item
            obj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(controller.OnItemClicked);
        }
    }



    public void SetInventoryItems()
    {
        

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
}

