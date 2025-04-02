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
        //gameobject MOET een rigibody EN collider hebben
        //if (EquippedItemManager.Instance.EquippedItemName == Item.name) // als je klikt op een item kan di oppaken en unequippen betekenen, drm deze controle
        //{ //probleem als je een item met dezelfde naam oppakt, dan wordt deze toch zo gezegd unequiped
        //    unequip();
        //}
        //else
        //{
        //    Pickup();

        //}
        Pickup(); // opl : ?? -> check fps controller , controle of inventory open is }  
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && EquippedItemManager.Instance.EquippedItemName == Item.name)
        {
            unequip();
            Debug.Log("a");
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