using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임의 흐름 제어, 다른 매니저 클래스 관리
/// </summary>
[RequireComponent(typeof(UIManager))]
public class GameManager : Singleton<GameManager>
{
    public UIManager UIManager { get; private set; }

    protected override void Initialize()
    {
        UIManager = GetComponent<UIManager>();
    }
}
