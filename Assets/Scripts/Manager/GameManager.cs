using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 게임의 흐름 제어, 다른 매니저 클래스 관리
/// </summary>
[RequireComponent(typeof(UIManager))]
public class GameManager : Singleton<GameManager>
{
    public UIManager UIManager { get; private set; }

    private void Start()
    {
        SetCursor(true);
    }

    protected override void Initialize()
    {
        UIManager = GetComponent<UIManager>();
    }

    public void SetCursor(bool isLock)
    {
        if(isLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
