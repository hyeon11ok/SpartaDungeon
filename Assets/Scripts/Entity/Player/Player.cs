using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(PlayerController), typeof(PlayerCondition))]
public class Player : MonoBehaviour
{
    public PlayerController Controller { get; private set; }
    public PlayerCondition Condition { get; private set; }

    private void Awake()
    {
        Controller = GetComponent<PlayerController>();
        Condition = GetComponent<PlayerCondition>();
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

    public void OnLookInput(InputAction.CallbackContext context)
    {
        Controller.SetLookDelta(context.ReadValue<Vector2>());
    }

    private bool TypeCheck<T>(GameObject component)
    {
        return component.GetComponent<T>() != null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(TypeCheck<ICollsionEnter>(collision.gameObject))
        {
            collision.gameObject.GetComponent<ICollsionEnter>().EnterEvent(gameObject);
        }
    }
}
