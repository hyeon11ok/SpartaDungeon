using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 상호 작용 가능한 오브젝트를 나타내는 인터페이스입니다.
/// </summary>
public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
}

/// <summary>
/// 아이템 오브젝트를 나타내는 클래스입니다.
/// </summary>
public abstract class ItemObject : MonoBehaviour, IInteractable
{

    public abstract string GetInteractPrompt();

    public virtual void OnInteract()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 대상 오브젝트가 Awake()가 완료될 때까지 대기합니다.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    protected IEnumerator WaitForSubscribe(Action action)
    {
        yield return new WaitUntil(() => CharacterManager.IsAwakeDone);
        action();
    }
}
