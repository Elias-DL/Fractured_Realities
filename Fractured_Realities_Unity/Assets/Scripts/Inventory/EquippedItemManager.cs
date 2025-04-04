using UnityEngine;

public class EquippedItemManager : MonoBehaviour
{
    public static EquippedItemManager Instance { get; private set; }
    private Item equippedItem;
    private GameObject equippedItemInstance; // Store the actual GameObject

    public string EquippedItemName => equippedItem != null ? equippedItem.itemName : "";

    private void Update()
    {
        if (equippedItem != null)
        {
            Debug.Log(equippedItem.name);

        }
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetEquippedItem(Item item, GameObject instance)
    {
        equippedItem = item;
        equippedItemInstance = instance; // Store the actual instantiated object
    }

    public Item GetEquippedItem()
    {
        return equippedItem;
    }

    public GameObject GetEquippedItemInstance()
    {
        return equippedItemInstance; // Return the instantiated GameObject
    }

    public void ClearEquippedItem()
    {
        equippedItem = null;
        equippedItemInstance = null;
    }
}