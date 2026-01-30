using System;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerInputReader : MonoBehaviour, PlayerInputController.IMovementActions
{
    public event Action<bool> OnInteractEvent;
    
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;

    private PlayerInputController m_playerInputController;

    private void Awake()
    {
        m_playerInputController = new PlayerInputController();
        m_playerInputController.Movement.SetCallbacks(this);
    }

    private void OnEnable()
    {
        m_playerInputController.Movement.Enable();
    }

    private void OnDestroy()
    {
        m_playerInputController.Movement.Disable();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInteractEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnInteractEvent?.Invoke(false);
        }
    }

    public void OnMovment(InputAction.CallbackContext context)
    {
        if (!context.performed && !context.canceled) return;
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }    

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!context.performed && !context.canceled) return;
        OnLookEvent?.Invoke(context.ReadValue<Vector2>());
    }
}