using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static string spawnPointName; // Naam van het teleportatiedoel

    private void Awake()
    {
        if (!string.IsNullOrEmpty(spawnPointName))
        {
            // Zoek het teleportatiedoel op basis van zijn naam
            GameObject spawnPoint = GameObject.Find(spawnPointName);

            if (spawnPoint != null)
            {
                // Zoek de speler en verplaats deze naar het teleportatiedoel
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    player.transform.position = spawnPoint.transform.position; // Verplaats speler naar de doelpositie
                    player.transform.rotation = spawnPoint.transform.rotation; // Stel de rotatie in
                }
                else
                {
                    Debug.LogError("Speler met tag 'Player' niet gevonden in de scene.");
                }
            }
            else
            {
                Debug.LogError($"Teleportatiedoel '{spawnPointName}' niet gevonden in de scene.");
            }
        }
        else
        {
            Debug.LogWarning("Er is geen spawnPointName opgegeven. De speler spawnt op de standaardpositie.");
        }
    }
}
