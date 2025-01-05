using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorAynur : MonoBehaviour
{
    public string targetScene; // De scene waar deze deur naartoe leidt
    public string doorName;    // Unieke naam van deze deur

    private void OnMouseDown()
    {
        // Opslaan in GameManager
        GameManager.Instance.lastPlayerPosition = transform.position;
        GameManager.Instance.lastDoorName = doorName;

        // Scene laden
        SceneManager.LoadScene(targetScene);
    }
}
