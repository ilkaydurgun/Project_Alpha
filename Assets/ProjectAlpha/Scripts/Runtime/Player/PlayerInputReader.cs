using System;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerInputReader : MonoBehaviour,PlayerInputController.IMovementActions
{
    public event Action OnInteractEvent;
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;

    private PlayerInputController playerInputController;

    private void Awake()
    {
         playerInputController = new PlayerInputController();
          playerInputController.Movement.SetCallbacks(this);
    }
    private void OnEnable()
    {
        playerInputController.Movement.Enable();
    }
    private void OnDestroy()
    {
        playerInputController.Movement.Disable();
    }
     public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) {return;}
        OnInteractEvent?.Invoke();
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
