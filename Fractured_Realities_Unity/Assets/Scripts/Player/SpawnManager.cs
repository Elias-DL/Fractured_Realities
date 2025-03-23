using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public static string spawnPointName; // Naam van het teleportatiedoel
    int teller = 0;// soms fout dat else toch wordt opgeroepen, mogelijke opl


    Scene currentScene;
    string currentSceneName;


    public void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;

        if (currentSceneName != "Main Menu")
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
                        CharacterController CC = player.GetComponent<CharacterController>();
                        CC.enabled = false;
                        player.transform.position = spawnPoint.transform.position; // Verplaats speler naar de doelpositie
                        player.transform.rotation = spawnPoint.transform.rotation; // Stel de rotatie in
                        CC.enabled = true;
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
                // Zoek de speler en verplaats deze naar het teleportatiedoel
                GameObject player = GameObject.FindWithTag("Player");
                GameObject startspawn = GameObject.FindWithTag("StartSpawn");
                player.transform.position = startspawn.transform.position;
                player.transform.rotation = startspawn.transform.rotation;
                Debug.LogWarning("Er is geen spawnPointName opgegeven. De speler spawnt op de standaardpositie.");
            }
        }
    }


}
