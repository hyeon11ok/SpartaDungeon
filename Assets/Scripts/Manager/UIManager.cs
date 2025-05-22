using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Condition,
    Interact
}

public class UIManager :MonoBehaviour
{
    private Dictionary<UIState, BaseUI> uiDictionary;

    private void Awake()
    {
        uiDictionary = new Dictionary<UIState, BaseUI>();
    }

    public void SetUI(UIState uiState, BaseUI ui)
    {
        if(uiDictionary == null)
            uiDictionary = new Dictionary<UIState, BaseUI>();

        uiDictionary.Add(uiState, ui);
    }

    public void OpenUI(UIState uiState)
    {
        uiDictionary[uiState].SetUIActive(true);
    }

    /// <summary>
    /// 현재 UI 비활성화
    /// </summary>
    public void CloseUI(UIState uiState)
    {
        uiDictionary[uiState].SetUIActive(false);
    }

    /// <summary>
    /// 현재 UI 비활성화 후 다음 UI 활성화
    /// </summary>
    /// <param name="uiState">다음 UI</param>
    public void ChangeUI(UIState openState, UIState closeState)
    {
        CloseUI(closeState);
        OpenUI(openState);
    }
}
