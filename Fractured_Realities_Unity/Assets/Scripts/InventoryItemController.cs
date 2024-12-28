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
    }

    public void RecreateItemInWorld()
    {
        if (itemPrefab != null)
        {
            // You can modify this position to be wherever you want the item to appear
            Vector3 spawnPosition = GetDropPosition();

            // Instantiate the item in the world
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Item prefab is not assigned!");
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
