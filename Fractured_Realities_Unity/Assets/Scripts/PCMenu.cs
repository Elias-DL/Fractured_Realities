using UnityEngine;

public class PCMenu : MonoBehaviour
{
    public GameObject menuUI; // Assign the Panel in Inspector

    private void OnMouseDown()
    {
        if (menuUI != null)
        {
            menuUI.SetActive(!menuUI.activeSelf); // Toggle menu visibility
        }
    }
}
