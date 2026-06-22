using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private const string PLAYER_BINGINGS_PREFS = "InputBindings";

    private Inputs playerInputActions;
    public event EventHandler OnInteractionHandler;

    public void Awake()
    {
        Instance = this;
        playerInputActions = new Inputs();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interaction.performed += Interact_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Dispose();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractionHandler?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
