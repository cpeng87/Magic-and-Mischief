using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Placeable", menuName = "Inventory/PlaceableData")]
public class PlaceableData : SellableItemData
{
    public Vector2 size;
    public GameObject placedItem;
}

