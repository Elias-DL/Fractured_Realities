using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public Item item; // Reference to the item data
    public GameObject itemPrefab; // Prefab of the item
    public Transform player; // Reference to the player's transform
    public Vector3 offset = new Vector3(10,20,20); // Offset position relative to the player

    private bool isEquipped = false; // Flag to check if the item is equipped

    // Triggered when the item UI is clicked
    public void OnItemClick()
    {
        if (!isEquipped)
        {
            EquipItem();
        }
        else
        {
            UnequipItem();
        }
    }
    private void Update()
    {
        if (isEquipped && player != null)
        {
            transform.position = player.position + offset;
        }
    }

    // Equip the item to the player
    public void EquipItem()
    {
        isEquipped = true;

        if (player != null)
        {
            // Set item's position relative to the player
            transform.SetParent(player);
            transform.localPosition = offset;

            // Disable physics if the item has a Rigidbody
            if (TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
        }
        else
        {
            Debug.LogWarning("Player transform is not assigned!");
        }

        InventoryManager.Instance.Remove(item); // Remove the item from inventory
        Destroy(gameObject); // Remove the item's UI element
    }

    // Unequip the item and place it back in the world
    public void UnequipItem()
    {
        isEquipped = false;

        // Detach the item from the player
        transform.SetParent(null);

        // Re-enable physics if the item has a Rigidbody
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }

        // Recreate the item in the world
        RecreateItemInWorld();
    }

    // Adds a new item to the inventory
    public void AddItem(Item newItem)
    {
        item = newItem;

        // Assign the dynamic item prefab from the item data
        if (newItem.prefab != null)
        {
            itemPrefab = newItem.prefab;
        }
        else
        {
            Debug.LogWarning("Item does not have an associated prefab!");
        }
    }

    // Removes the item from the inventory
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        RecreateItemInWorld(); // Spawn it back in the world
        Destroy(gameObject); // Remove the item's UI representation
    }

    // Recreates the item as a GameObject in the world
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

    // Calculates the drop position slightly in front of the player
    public Vector3 GetDropPosition()
    {
        // Get player position (assuming the player has a tag "Player")
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            // Drop the item slightly in front of the player
            Vector3 playerPosition = playerObject.transform.position;
            Vector3 forward = playerObject.transform.forward;
            return playerPosition + forward * 2; // Adjust the 2 for drop distance
        }
        else
        {
            Debug.LogWarning("Player not found. Using default spawn position.");
            return Vector3.zero;
        }
    }
}
