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
        InventoryManager.Instance.Remove(item);
    }

    public void EquipItem()
    {
        if (item != null && item.prefab != null)
        {
            // Het vorige equippte item verwijderen
            GameObject previousEquippedItemObject = EquippedItemManager.Instance.GetEquippedItemInstance();
            if (previousEquippedItemObject != null)
            {
                Item previousEquippedItem = EquippedItemManager.Instance.GetEquippedItem();
                if (previousEquippedItem != null)
                {
                    InventoryManager.Instance.Add(previousEquippedItem); // Het vorige item terug in inventory steken
                }
                Destroy(previousEquippedItemObject); // Het vorige equippte item verwijderen
                EquippedItemManager.Instance.ClearEquippedItem();
            }

            // De positie waar de items worden "vast gepakt" bepalen
            Vector3 equipPoint = GameObject.FindGameObjectWithTag("EquipPosition").transform.position;
            Quaternion rotation = Quaternion.Euler(0, 0, 90);

            // Een nieuwe versie van het item maken voor in de map te tonen
            GameObject newEquippedItemObject = Instantiate(item.prefab, equipPoint, rotation);
            newEquippedItemObject.transform.parent = player;

            // Na dat het item in de wereld is gezet roteren zodat deze goed zichtbaar is
            if (item.name == "Flashlight")
                rotation = Quaternion.Euler(0, 90, 90);
            else if (item.name.Contains("Candle") || item.name == "Zombie" || item.name == "Bookhead" || item.name == "Anklegrabber" || item.name.Contains("Painting"))
                rotation = Quaternion.Euler(0, 0, 0);
            else if (item.name == "FinalKey")
                rotation = Quaternion.Euler(270, -90, -90);
            else if (item.name == "Camera")
                rotation = Quaternion.Euler(-180, 0, 0);

            newEquippedItemObject.transform.localRotation = rotation;

            // De rb op kinematic zetten
            Rigidbody rb = newEquippedItemObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Het nietuwe item als equipped bijhouden
            EquippedItemManager.Instance.SetEquippedItem(item, newEquippedItemObject);
            EquippedItemName = item.itemName;
        }
        else
        {
            Debug.LogWarning("Item or prefab mist!");
            EquippedItemManager.Instance.ClearEquippedItem();
            EquippedItemName = "";
        }
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        if (item != null && item.prefab != null)
        {
            itemPrefab = item.prefab; // De juiste prefab linken indien nodig
            //Debug.Log("add");
        }
        else
        {
            Debug.LogWarning("Item or prefab mist voor " + item.itemName);
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
            Debug.LogWarning("Item prefab is niet in inspector ingevuld");
        }
    }


    public Vector3 GetDropPosition()
    {
        //Pos van de speler
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