using UnityEngine;

public class EquippedItemManager : MonoBehaviour
{
    public static EquippedItemManager Instance { get; private set; }
    private Item equippedItem;
    private GameObject equippedItemInstance; // Gameobject van het item

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
        equippedItemInstance = instance;
    }

    public Item GetEquippedItem()
    {
        return equippedItem;
    }

    public GameObject GetEquippedItemInstance()
    {
        return equippedItemInstance; 
    }

    public void ClearEquippedItem()
    {
        equippedItem = null;
        equippedItemInstance = null;
    }
}