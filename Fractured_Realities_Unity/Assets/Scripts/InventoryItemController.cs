using UnityEngine;

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

    private void Start()
    {
        // Get the player's transform (assuming the player has a tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Continuously rotate the equipped item (if any)
        if (equippedItem != null)
        {
            equippedItem.transform.Rotate(rotationSpeed * Time.deltaTime);

            // Update position and rotation based on adjustments
            UpdateEquippedItemPosition();
            UpdateEquippedItemRotation();
        }
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
            // Destroy any currently equipped item before equipping a new one
            if (equippedItem != null)
            {
                Destroy(equippedItem);
            }

            // Instantiate the item's prefab
            equippedItem = Instantiate(item.prefab);
            equippedItem.transform.position = player.position + player.forward * offsetPosition.z + Vector3.up * offsetPosition.y;

            // Parent it to the player for it to follow the player
            equippedItem.transform.parent = player;

            // Disable physics on equipped item (optional)
            Rigidbody rb = equippedItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
        else
        {
            Debug.LogWarning("Item or prefab is missing for this item!");
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
        }
        else
        {
            Debug.LogWarning("Item or prefab is missing for " + item.itemName);
        }
    }


    // Update equipped item position based on player and adjustments
    private void UpdateEquippedItemPosition()
    {
        if (equippedItem != null)
        {
            equippedItem.transform.position = player.position + player.forward * offsetPosition.z + Vector3.up * offsetPosition.y;
            equippedItem.transform.position += manualPositionAdjustments;
        }
    }

    // Update equipped item rotation based on adjustments
    private void UpdateEquippedItemRotation()
    {
        if (equippedItem != null)
        {
            equippedItem.transform.rotation = Quaternion.Euler(manualRotationAdjustments);
        }
    }
}
