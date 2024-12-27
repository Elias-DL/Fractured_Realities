using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string doorName; // Unique name for the door.
    public string targetScene; // The name of the target scene.
    public Vector3 targetSpawnPoint; // Position in the target scene to spawn at.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Save the door name and target spawn point to TeleportManager.
            TeleportManager.Instance.SetLastDoorUsed(doorName, targetSpawnPoint);

            // Load the target scene.
            SceneManager.LoadScene(targetScene);
        }
    }
}
