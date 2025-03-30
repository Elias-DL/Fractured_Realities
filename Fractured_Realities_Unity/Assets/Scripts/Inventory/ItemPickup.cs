using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject MainCamera;

    public Item Item;
    // Start is called before the first frame update
    void Pickup()
    {
        MainCamera = GameObject.FindWithTag("MainCamera");
        int defaultLayer = LayerMask.NameToLayer("Default");
    
        MainCamera.layer = defaultLayer;
        if (EquippedItemManager.Instance.EquippedItemName != null)
        {
            EquippedItemManager.Instance.ClearEquippedItem();
        }
        
        EquippedItemManager.Instance.ClearEquippedItem(); // zodat de item niet equipped is als je die terug is inventory zet
        InventoryManager.Instance.Add(Item);
        InventoryManager.Instance.ListItems(); // UI refreshed
        Destroy(gameObject);
    }


    // Update is called once per frame
    private void OnMouseDown() // gameobject MOET een rigibody EN collider hebben
    {

        Pickup();
        
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && EquippedItemManager.Instance.EquippedItemName == Item.name)
       {
            Pickup();
            Debug.Log("a");
        }
    }
}