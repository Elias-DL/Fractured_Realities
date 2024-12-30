using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public GameObject itemPrefab; 

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        RecreateItemInWorld();
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        // Assign the dynamic item prefab from the Item data
        if (newItem.prefab != null)
        {
            itemPrefab = newItem.prefab;
        }
        else
        {
            Debug.LogWarning("Item does not have an associated prefab!");
        }
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
