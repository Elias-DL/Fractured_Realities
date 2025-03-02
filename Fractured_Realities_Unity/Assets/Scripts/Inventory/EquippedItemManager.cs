using UnityEngine;

public class EquippedItemManager : MonoBehaviour
{
    public static EquippedItemManager Instance { get; private set; }

    public string EquippedItemName { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        if (EquippedItemName != null)
        {
            DontDestroyOnLoad(gameObject); // Keep this object persistent across scenes
        }
    }

    public void SetEquippedItem(string itemName)
    {
        EquippedItemName = itemName;
    }

    public void ClearEquippedItem()
    {
        EquippedItemName = null;
    }
}
