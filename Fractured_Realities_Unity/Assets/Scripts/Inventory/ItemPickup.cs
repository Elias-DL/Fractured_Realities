using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class ItemPickup : MonoBehaviour
{
    public GameObject MainCamera; public Item Item;
    public TextMeshProUGUI txtTips;

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
       
        Destroy(gameObject);
        InventoryManager.Instance.Add(Item);

        InventoryManager.Instance.ListItems(); 
    }
}