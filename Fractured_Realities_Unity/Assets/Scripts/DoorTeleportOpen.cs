using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleportOpen : MonoBehaviour
{
    public string targetScene; // De scene waar deze deur naartoe leidt
    public string doorName;    // Unieke naam van deze deur

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger
        if (other.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
        
                // Opslaan in GameManager
                GameManager.Instance.lastPlayerPosition = transform.position;
                GameManager.Instance.lastDoorName = doorName;

                // Scene laden
                SceneManager.LoadScene(targetScene);
            Debug.Log("GAA TNEIUT");
            
        }
        else
        {
            Debug.LogError("Scene name is not set in the inspector!");
        }
    }
}
