using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // For the new Unity Input System

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // The name of the scene to load

    public void OnMouseDown()
    {
        // Check if the left mouse button was clicked
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            LoadScene();
           
        }
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is not set in the inspector!");
        }
    }
}