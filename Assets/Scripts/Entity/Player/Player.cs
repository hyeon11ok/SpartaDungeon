using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(PlayerController), typeof(PlayerCondition))]
public class Player:MonoBehaviour
{
    public PlayerController Controller { get; private set; }
    public PlayerCondition Condition { get; private set; }

    private void Awake()
    {
        Controller = GetComponent<PlayerController>();
        Condition = GetComponent<PlayerCondition>();
    }

    public void OnBuffStart(ItemDataBuff[] buffs, float buffDuration)
    {
        Debug.Log("Buff Start");
        StartCoroutine(BuffCoroutine(buffs, buffDuration));
    }

    /// <summary>
    /// 버프를 활성화하거나 비활성화합니다.
    /// </summary>
    /// <param name="buffs">버프 목록</param>
    /// <param name="isActive">활성화 유무</param>
    private void BuffActive(ItemDataBuff[] buffs, bool isActive)
    {
        foreach(var buff in buffs)
        {
            switch(buff.type)
            {
                case BuffType.Speed:
                    Controller.AddSpeed(isActive ? buff.value : -buff.value);
                    break;
            }
        }
    }

    private IEnumerator BuffCoroutine(ItemDataBuff[] buffs, float buffDuration)
    {
        BuffActive(buffs, true);
        yield return new WaitForSeconds(buffDuration);
        BuffActive(buffs, false);
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

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            Controller.SwitchRunMode(true);
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            Controller.SwitchRunMode(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(TypeCheck<ICollsionEnter>(collision.gameObject))
        {
            collision.gameObject.GetComponent<ICollsionEnter>().EnterEvent(gameObject);
        }
    }
}
