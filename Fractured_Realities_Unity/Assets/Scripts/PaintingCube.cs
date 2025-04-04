using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingCube : MonoBehaviour
{

    public GameObject Image;
    public string painting;

    private bool isPlaced = false;

    private void OnMouseDown()
    {
        if (EquippedItemManager.Instance.EquippedItemName == painting)
        {
            Image.SetActive(true);
            EquippedItemManager.Instance.ClearEquippedItem();
            Debug.Log(painting);

            isPlaced = true; // Mark as placed
            GameManager.Instance.CheckPaintings(); // Notify GameManager
        }
    }
}
