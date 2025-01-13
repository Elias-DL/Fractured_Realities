using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleport : MonoBehaviour
{
    public string targetScene;          // Naam van de nieuwe scene
    public string targetSpawnPointName; // Naam van het teleportatiedoel in de nieuwe scene

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Stel de spawnpointnaam in voordat de scene wordt geladen
            SpawnManager.spawnPointName = targetSpawnPointName;

            // Laad de nieuwe scene
            SceneManager.LoadScene(targetScene);
        }
    }
}
