using PlayerControlsScript;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerInputController : MonoBehaviour // Script voor het gebruiken van het nieuwe input systeem
{
    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.ToggleInventory.performed += ctx => ToggleInventory();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void ToggleInventory()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.ToggleInventory();
        }
    }
}
