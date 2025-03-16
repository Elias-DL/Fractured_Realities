using UnityEngine;


public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public GameObject itemPrefab;
    public Transform player;
    private GameObject equippedItem;



    // Static reference to the currently equipped item's name
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

    // Equip the item in front of the player
    public void EquipItem()
    {
        if (item != null && item.prefab != null)
        {
            if (equippedItem != null)
            {
                Destroy(equippedItem);
            }



            Vector3 Equippoint = GameObject.FindGameObjectWithTag("EquipPosition").transform.position;
            Quaternion Rotation = Quaternion.Euler(0, 0, 90);
            Debug.Log("item op pos : " + Equippoint + " en rotatie " + Rotation);

            equippedItem = Instantiate(item.prefab, Equippoint, Rotation);
            equippedItem.transform.parent = player;
            item.itemName = item.name;

            Debug.Log(item.name);   


            // niet de beste manier maar kijk het werkt(voor nu), gwn elke keer kijken welke het is aangezien veel items anders moeten gepositioneerd worden, makkelijker dan prefab want rotatation moet in code gedefinieerd worden anders is het item verkeerd gedraaid
            if (item.name == "Flashlight")
            {
                Rotation = Quaternion.Euler(0, 90, 90);

            }
            else if (item.name.Contains("Candle") )
            {
                Rotation = Quaternion.Euler(0, 0, 0);


            }

            else if (item.name == "Key6")
            {
                Rotation = Quaternion.Euler(270, -90, -90);

            }
            else
            {
                Rotation = Quaternion.Euler(0, 0, 90);
            }


            equippedItem.transform.localRotation = Rotation; // Resets local rotation

            Rigidbody rb = equippedItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            EquippedItemManager.Instance.SetEquippedItem(item.itemName); // Notify the manager
        }
        else
        {
            Debug.LogWarning("Item or prefab is missing for this item!");
            EquippedItemManager.Instance.ClearEquippedItem(); // Clear if nothing is equipped
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
            Debug.Log("add");
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