using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Plantable", menuName = "Inventory/PlantableData")]
public class PlantableData : SellableItemData
{
    public string harvestItemName;
    public int growthTime; //in days
    public Tile[] growthTiles;
}