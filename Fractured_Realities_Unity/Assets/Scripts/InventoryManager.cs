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

    private void Awake()
    {
        Instance = this;
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
        // If you prefer to use the old Input system
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (InventoryPanel != null)
        {
            bool isActive = InventoryPanel.activeSelf;
            InventoryPanel.SetActive(!isActive);

            if (!isActive)
            {
                ListItems();
            }
        }
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }

        SetInventoryItems();
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
