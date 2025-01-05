using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform defaultSpawnPoint; // Als er geen specifieke deur is, gebruik dit punt
    public Door[] doors;               // Alle deuren in deze scene

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player"); // Zoek de speler

        // Controleer of er een opgeslagen deur is
        if (!string.IsNullOrEmpty(GameManager.Instance.lastDoorName))
        {
            foreach (Door door in doors)
            {
                if (door.doorName == GameManager.Instance.lastDoorName)
                {
                    // Zet de speler bij de juiste deur
                    player.transform.position = door.transform.position;
                    return;
                }
            }
        }

        // Geen deur gevonden? Gebruik standaard spawnpunt
        player.transform.position = defaultSpawnPoint.position;
    }
}
