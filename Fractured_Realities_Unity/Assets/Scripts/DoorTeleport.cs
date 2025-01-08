using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // For the new Unity Input System

public class DoorInteraction : MonoBehaviour
{
    
    [SerializeField] private string neccescaryKey;
    public string targetScene; // De scene waar deze deur naartoe leidt
    public string doorName;    // Unieke naam van deze deur


    public void OnMouseDown()
    {
        // Check if the left mouse button was clicked
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            string equippedItem = EquippedItemManager.Instance.EquippedItemName;
            if (equippedItem == neccescaryKey) // in inspector bv : Key4, volledig niet alleen getal
            {
             
                
                    // Opslaan in GameManager
                    GameManager.Instance.lastPlayerPosition = transform.position;
                    GameManager.Instance.lastDoorName = doorName;

                    // Scene laden
                    SceneManager.LoadScene(targetScene);
                

            }
            else
            {
                if (equippedItem == null)
                {
                    Debug.Log("NO KEYS????");

                }
                else
                {
                    Debug.Log("You don't have the necessary key equipped! , you have " + equippedItem + " and you need key " + neccescaryKey);

                }
            }

        }
    }
}