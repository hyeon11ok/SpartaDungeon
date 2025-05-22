using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        OnBuffActive -= CharacterManager.Instance._Player.OnBuffStart;
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
