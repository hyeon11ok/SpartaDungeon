using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public PlayerController Controller { get; private set; }

    private void Awake()
    {
        Controller = GetComponent<PlayerController>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Controller.SetDir(context.ReadValue<Vector2>());
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            Controller.SetDir(Vector2.zero);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            Controller.IsInputJumpingAction(true);
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            Controller.IsInputJumpingAction(false);
        }
    }
}
