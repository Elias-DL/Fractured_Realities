using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    // Start is called before the first frame update
    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        InventoryManager.Instance.ListItems(); // UI refreshed direct   
        Destroy(gameObject);
    }

    // Update is called once per frame
    private void OnMouseDown() // probkeem -> altijd click niet alleen op object? (gefixed zie inventorymanager)
    {
        Pickup();
    }
}

