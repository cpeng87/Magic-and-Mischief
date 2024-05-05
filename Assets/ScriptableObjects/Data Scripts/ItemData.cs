using UnityEngine;
using System;

public abstract class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public string description;
    public Sprite icon;
}
