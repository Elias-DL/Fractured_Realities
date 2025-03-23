using PlayerControlsScript;
using UnityEngine;
using UnityEngine.InputSystem; // Required for the new Input System

public class PlayerInputController : MonoBehaviour
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
