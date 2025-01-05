using UnityEngine;
using static UnityEditor.Progress;

public class InventoryItemController : MonoBehaviour
{
    public Item item; // Reference to the item data (ScriptableObject)
    public GameObject itemPrefab; // Prefab of the item (from the item data)
    public Transform player; // Reference to the player's transform
    private GameObject equippedItem; // Reference to the equipped item instance

    [Header("Position and Rotation Settings")]
    public Vector3 offsetPosition = new Vector3(0f, 1.5f, 1.5f); // Position offset for equipping
    public Vector3 rotationSpeed = new Vector3(0f, 30f, 0f); // Rotation speed for equipped items

    [Header("Manual Adjustments")]
    public Vector3 manualPositionAdjustments; // Fine-tuning item position
    public Vector3 manualRotationAdjustments; // Fine-tuning item rotation

    // Static reference to the currently equipped item's name
    public static string EquippedItemName { get; private set; }

    private void Start()
    {
        // Get the player's transform (assuming the player has a tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //// Continuously rotate the equipped item (if any)
        //if (equippedItem != null)
        //{
        //    equippedItem.transform.Rotate(rotationSpeed * Time.deltaTime);

        //    // Update position and rotation based on adjustments
        //    UpdateEquippedItemPosition();
        //    UpdateEquippedItemRotation();
        //}
    }

    // This is called when an item is clicked in the inventory UI
    public void OnItemClicked()
    {
        EquipItem();
        InventoryManager.Instance.Remove(item); // Remove from inventory (but no world drop)
    }

    // Equip the item in front of the player
    public void EquipItem()
    {
        if (item != null && item.prefab != null)
        {
            if (equippedItem != null)
            {
                Destroy(equippedItem);
            }

            Vector3 Equippoint = GameObject.FindGameObjectWithTag("EquipPosition").transform.position;
            Debug.Log(Equippoint);
            equippedItem = Instantiate(item.prefab, Equippoint, Quaternion.identity);
            equippedItem.transform.parent = player;
            item.itemName = item.name;
            Rigidbody rb = equippedItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            EquippedItemManager.Instance.SetEquippedItem(item.itemName); // Notify the manager
        }
        else
        {
            Debug.LogWarning("Item or prefab is missing for this item!");
            EquippedItemManager.Instance.ClearEquippedItem(); // Clear if nothing is equipped
        }
    }


    // Adds an item to this controller, ensuring the correct prefab is linked
    public void AddItem(Item newItem)
    {
        // Assign the item data
        item = newItem;

        // Assign the prefab from the item's data
        if (item != null && item.prefab != null)
        {
            itemPrefab = item.prefab; // Assign the correct prefab
            Debug.Log("add");
        }
        else
        {
            Debug.LogWarning("Item or prefab is missing for " + item.itemName);
        }
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        RecreateItemInWorld();
        Destroy(gameObject);
    }

    public void RecreateItemInWorld()
    {
        if (itemPrefab != null)
        {
            Vector3 spawnPosition = GetDropPosition();
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Item prefab is not assigned for this item!");
        }
    }


    public Vector3 GetDropPosition()
    {
        // Get player position (assuming the player has a tag "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Drop the item slightly in front of the player
            Vector3 playerPosition = player.transform.position;
            Vector3 forward = player.transform.forward;
            return playerPosition + forward * 2; // Adjust the 2 to control how far in front the item appears
        }
        else
        {
            Debug.LogWarning("Player not found. Using default spawn position.");
            return Vector3.zero;
        }
    }
}
    // Update equipped item position based on player and adjustments
    //private void UpdateEquippedItemPosition()
    //{
    //    if (equippedItem != null)
    //    {
    //        equippedItem.transform.position = player.position + player.forward * offsetPosition.z + Vector3.up * offsetPosition.y * 5;
    //        equippedItem.transform.position += manualPositionAdjustments;
    //    }
    //}

    //// Update equipped item rotation based on adjustments
    //private void UpdateEquippedItemRotation()
    //{
    //    if (equippedItem != null)
    //    {
    //        equippedItem.transform.rotation = Quaternion.Euler(manualRotationAdjustments);
    //    }
    //}

