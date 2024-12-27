using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    public GameObject player; // Reference to the player object.
    public Transform door1SpawnPoint; // Transform for Door 1's spawn location.
    public Transform door2SpawnPoint; // Transform for Door 2's spawn location.

    private void Start()
    {
        // Get the last door used.
        string lastDoor = TeleportManager.Instance.GetLastDoorUsed();

        // Default to door 1 if no door is specified.
        Vector3 spawnLocation = door1SpawnPoint.position;

        // Adjust the spawn location based on the last door used.
        if (lastDoor == "Door2")
        {
            spawnLocation = door2SpawnPoint.position;
        }

        // Move the player to the spawn location.
        player.transform.position = spawnLocation;
    }
}
