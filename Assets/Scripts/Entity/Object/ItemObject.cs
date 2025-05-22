using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
}

public abstract class ItemObject : MonoBehaviour, IInteractable
{

    public abstract string GetInteractPrompt();

    public virtual void OnInteract()
    {
        Destroy(gameObject);
    }

    protected IEnumerator WaitForSubscribe(Action action)
    {
        yield return new WaitUntil(() => CharacterManager.IsAwakeDone);
        action();
    }
}
