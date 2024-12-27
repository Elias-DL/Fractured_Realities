using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;

    private string lastDoorUsed; // Tracks the name of the last door the player used.
    private Vector3 spawnLocation;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Set the last door used.
    public void SetLastDoorUsed(string doorName, Vector3 location)
    {
        lastDoorUsed = doorName;
        spawnLocation = location;
    }

    // Get the last door used.
    public string GetLastDoorUsed()
    {
        return lastDoorUsed;
    }

    // Get the spawn location.
    public Vector3 GetSpawnLocation()
    {
        return spawnLocation;
    }
}
