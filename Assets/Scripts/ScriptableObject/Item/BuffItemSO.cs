using System;
using UnityEngine;

public enum BuffType
{
    Speed,
    Stamina
}

[Serializable]
public struct ItemDataBuff
{
    public BuffType type;
    public float value;
}

[CreateAssetMenu(fileName = "BuffItemSO", menuName = "ScriptableObjects/BuffItemSO", order = int.MinValue)]
public class BuffItemSO:ItemData
{
    [Header("Buff")]
    [SerializeField] private ItemDataBuff[] buffs;
    [SerializeField] private float buffDuration;

    public ItemDataBuff[] Buffs => buffs;
    public float BuffDuration => buffDuration;
}
