using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 버프 아이템을 나타내는 클래스입니다.
/// </summary>
public class BuffItem:ItemObject
{
    [SerializeField] private BuffItemSO data;
    public event Action<ItemDataBuff[], float> OnBuffActive;

    private void OnEnable()
    {
        StartCoroutine(WaitForSubscribe(() => OnBuffActive += CharacterManager.Instance._Player.OnBuffStart));
    }

    private void OnDisable()
    {
        OnBuffActive = null;
    }

    public override void OnInteract()
    {
        OnBuffActive?.Invoke(data.Buffs, data.BuffDuration);
        base.OnInteract();
    }

    public override string GetInteractPrompt()
    {
        string str = $"{data.Name}\n{data.Description}";
        return str;
    }
}
