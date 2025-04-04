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
        // OUd input systeem (beide kunnen tegelijk gebruikt worden)
        if (Input.GetKeyDown(KeyCode.E))  // De key button om het aan of uitzetten van de inventory te controleren
        {
            ToggleInventory();

        }
    }

    public void ToggleInventory()
    {

       
        bool isActive = Inventory.activeSelf;
        Inventory.SetActive(!isActive);

        // UI Refresh als de inventory wordt geopend
        if (!isActive)
        {
            ListItems();
        }

        // toggle inventory visibility check
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
        // Verwijder het item van de inventory
        Items.Remove(item);

        //  refresh inventory UI
        ListItems();
    }

    public void ListItems()
    {
       // Debug.Log("ListItems called");

        // Inventoy UI resetten
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        // De array leegmaken voor dat deze opnieuw wordt gebruikt.
        InventoryItems = new InventoryItemController[Items.Count];

        for (int i = 0; i < Items.Count; i++)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            obj.SetActive(true); 

            // Het icon en de naam van het item vinden zodat deze later kunnen worden ingesteld
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();

            // Instellen van naam en icon
            itemName.text = Items[i].itemName;
            itemIcon.sprite = Items[i].icon;

            obj.name = itemName.text;

            // Het item toevoegen aan de inventorycontroller
            InventoryItemController controller = obj.GetComponent<InventoryItemController>();

            // De corrosponderende items toevoegen aan de controller
            controller.AddItem(Items[i]);

            // De controller toevoegen op de juiste plek in de array
            InventoryItems[i] = controller;

            // Een event koppelen aan het item zodat je er op kan klikken om deze te equippen.
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

