using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 외부에서 플레이어 참조를 하기 위한 싱글톤 매니저
/// </summary>

[RequireComponent(typeof(Player))]
public class PlayerManager : Singleton<PlayerManager>
{
    public Player Player { get; private set; }

    protected override void Initialize()
    {
        Player = GetComponent<Player>();
    }
}
