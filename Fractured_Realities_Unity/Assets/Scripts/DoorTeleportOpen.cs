using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleportOpen : MonoBehaviour
{
    public string targetScene; // De scene waar deze deur naartoe leidt
    public string doorName;    // Unieke naam van deze deur


    private void OnTriggerEnter(Collider other)
    {
        // Controleer of de speler de trigger binnenkomt
        if (other.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        // Controleer of de scènenaam is ingesteld
        if (string.IsNullOrEmpty(targetScene))
        {
            Debug.LogError("Scènenaam is niet ingesteld in de Inspector!");
            return;
        }

        // Controleer of GameManager bestaat
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null! Zorg ervoor dat er een GameManager in de scène is.");
            return;
        }

        // Opslaan in GameManager
        Debug.Log($"Opslaan van spelerpositie: {transform.position}, deurnaam: {doorName}");
        GameManager.Instance.lastPlayerPosition = transform.position;
        GameManager.Instance.lastDoorName = doorName;

        // Scene laden
        Debug.Log($"Laden van scène: {targetScene} via deur: {doorName}");
        SceneManager.LoadScene(targetScene);
    }
}
