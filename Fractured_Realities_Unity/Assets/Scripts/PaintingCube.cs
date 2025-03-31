using UnityEngine;
using UnityEngine.InputSystem;

public class PaintingCube : MonoBehaviour
{
    [SerializeField] private string paintingName; // De juiste painting voor deze kubus
    [SerializeField] private Texture paintingTexture; // De painting texture voor deze kubus
    private bool isCorrect = false; // Houdt bij of deze kubus correct is

    private void OnMouseDown()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            string equippedItem = EquippedItemManager.Instance.EquippedItemName;

            if (equippedItem == paintingName) // Controleer of de juiste painting is geselecteerd
            {
                Debug.Log("Correct painting!");

                Renderer cubeRenderer = GetComponent<Renderer>();
                if (cubeRenderer != null)
                {
                    cubeRenderer.material.mainTexture = paintingTexture; // Zet de painting op de kubus
                }

                if (!isCorrect) // Alleen updaten als dit de eerste keer is
                {
                    isCorrect = true;
                    GameManager.Instance.CheckAllPaintings(); // Controleer of alle kubussen correct zijn
                }
            }
            else
            {
                Debug.Log("Wrong painting!");
            }
        }
    }

    public bool IsCorrect()
    {
        return isCorrect;
    }
}
