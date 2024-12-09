using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleportOpen : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // The name of the scene to load

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
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is not set in the inspector!");
        }
    }
}
