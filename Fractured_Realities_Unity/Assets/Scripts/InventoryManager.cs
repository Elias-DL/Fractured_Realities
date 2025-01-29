using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<Item> Items = new List<Item>();
    public Transform PlayerTransform;
    public GameObject itemPrefab;

    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject InventoryPanel; // Reference to the UI panel

    public InventoryItemController[] InventoryItems;

    public GameObject Player;
    public GameObject InventoryContent;


    public void Awake()
    {

        Instance = this;

        Player = GameObject.FindWithTag("Player");
        PlayerTransform = Player.transform;

        InventoryContent = GameObject.FindWithTag("InventoryContent");

        ItemContent = InventoryContent.transform;
    }

    private void Start()
    {
        if (InventoryPanel != null)
        {
            InventoryPanel.SetActive(false); // Hide inventory at start
        }
    }

    private void Update()
    {

        Player = GameObject.FindWithTag("Player");
        PlayerTransform = Player.transform;

        InventoryContent = GameObject.FindWithTag("InventoryContent");

        ItemContent = InventoryContent.transform;
        // If you prefer to use the old Input system
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        //if (InventoryPanel != null)
        //{
            bool isActive = InventoryPanel.activeSelf;
            InventoryPanel.SetActive(!isActive);

            // Only refresh the UI if the inventory is being opened
            if (!isActive)
            {
                ListItems(); // Refresh the inventory UI only when opening
            }
        //}
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
        Debug.Log("ListItems called");

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
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
}
