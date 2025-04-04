using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemPickup : MonoBehaviour
{
    public GameObject MainCamera; public Item Item; // Start is called before the first frame update
    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        InventoryManager.Instance.ListItems(); // Ui refresh
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
       
        Pickup(); 
    }


    private void Update()
    {
        //Debug.Log(Item.name);
        if (Input.GetKeyDown(KeyCode.R) && EquippedItemManager.Instance.EquippedItemName == Item.name)
        {
            unequip();
        }
    }
    void unequip()
    {
        MainCamera = GameObject.FindWithTag("MainCamera");
        int defaultLayer = LayerMask.NameToLayer("Default"); MainCamera.layer = defaultLayer; 
        EquippedItemManager.Instance.ClearEquippedItem();
        // zodat de item niet equipped is als je die terug is inventory zet InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
        InventoryManager.Instance.Add(Item);

        InventoryManager.Instance.ListItems(); // UI refreshed Destroy(gameObject); }
    }
}