using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 외부에서 플레이어 참조를 하기 위한 싱글톤 매니저
/// </summary>

public class CharacterManager : Singleton<CharacterManager>
{
    public Player _Player { get; private set; }

    protected override void Initialize()
    {
        _Player = GetComponent<Player>();
    }
}
