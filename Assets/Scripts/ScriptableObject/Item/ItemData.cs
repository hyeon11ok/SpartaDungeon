using System;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;

    public string Name => itemName;
    public string Description => itemDescription;
}


