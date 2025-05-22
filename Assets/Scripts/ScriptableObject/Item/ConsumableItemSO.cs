using System;
using UnityEngine;

[Serializable]
public struct ItemDataConsumable
{
    public ConditionType type;
    public float value;
}

[CreateAssetMenu(fileName = "ConsumableItemSO", menuName = "ScriptableObjects/ConsumableItemSO", order = int.MinValue)]
public class ConsumableItemSO:ItemData
{
    [Header("Consumable")]
    [SerializeField] private ItemDataConsumable[] consumables;
    public ItemDataConsumable[] Consumables => consumables;
}
