using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected abstract UIState UIState { get; }
    [SerializeField] protected bool isStartUI = false;
    public bool IsStartUI { get { return isStartUI; } }

    protected virtual void Start()
    {
        Initialize();

        if(isStartUI)
            GameManager.Instance.UIManager.OpenUI(UIState);
        else
            SetUIActive(false);
    }

    protected virtual void Initialize()
    {
        GameManager.Instance.UIManager.SetUI(UIState, this);
    }

    public virtual void SetUIActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
