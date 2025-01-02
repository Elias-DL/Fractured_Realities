using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset = new Vector3(1, 0, 0); // Offset position relative to the player
    private bool isEquipped = false; // Flag to check if the object is equipped

    private void OnMouseDown()
    {
        // Check if the object is clicked
        if (!isEquipped)
        {
            EquipF();
        }
        else
        {
            Unequip();
        }
    }

    private void Update()
    {
        if (isEquipped && player != null)
        {
            transform.position = player.position + offset;
        }
    }

    public void EquipF()
    {
        isEquipped = true;
        // valt niet
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
    }

    public void Unequip()
    {
        isEquipped = false;

        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }
    }
}
