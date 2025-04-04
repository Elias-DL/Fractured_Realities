using Unity.VisualScripting;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public GameObject itemPrefab;
    public Transform player;
    private GameObject equippedItem;

    public static string EquippedItemName { get; private set; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void OnItemClicked()
    {
        EquipItem();
        InventoryManager.Instance.Remove(item); // Remove from inventory (but no world drop)
    }

    public void EquipItem()
    {
        if (item != null && item.prefab != null)
        {
            // Retrieve and destroy the previously equipped item
            GameObject previousEquippedItemObject = EquippedItemManager.Instance.GetEquippedItemInstance();
            if (previousEquippedItemObject != null)
            {
                Item previousEquippedItem = EquippedItemManager.Instance.GetEquippedItem();
                if (previousEquippedItem != null)
                {
                    InventoryManager.Instance.Add(previousEquippedItem); // Return old item to inventory
                }
                Destroy(previousEquippedItemObject); // Destroy the actual instance in the scene
                EquippedItemManager.Instance.ClearEquippedItem();
            }

            // Determine the equip position and rotation
            Vector3 equipPoint = GameObject.FindGameObjectWithTag("EquipPosition").transform.position;
            Quaternion rotation = Quaternion.Euler(0, 0, 90);

            // Instantiate the new equipped item
            GameObject newEquippedItemObject = Instantiate(item.prefab, equipPoint, rotation);
            newEquippedItemObject.transform.parent = player;

            // Adjust rotation based on specific item names
            if (item.name == "Flashlight")
                rotation = Quaternion.Euler(0, 90, 90);
            else if (item.name.Contains("Candle") || item.name == "Zombie" || item.name == "Bookhead" || item.name == "Anklegrabber" || item.name.Contains("Painting"))
                rotation = Quaternion.Euler(0, 0, 0);
            else if (item.name == "FinalKey")
                rotation = Quaternion.Euler(270, -90, -90);
            else if (item.name == "Camera")
                rotation = Quaternion.Euler(-180, 0, 0);

            newEquippedItemObject.transform.localRotation = rotation;

            // Ensure the Rigidbody is kinematic
            Rigidbody rb = newEquippedItemObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Store the new equipped item in EquippedItemManager
            EquippedItemManager.Instance.SetEquippedItem(item, newEquippedItemObject);
            EquippedItemName = item.itemName;
        }
        else
        {
            Debug.LogWarning("Item or prefab is missing!");
            EquippedItemManager.Instance.ClearEquippedItem();
            EquippedItemName = "";
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
            //Debug.Log("add");
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