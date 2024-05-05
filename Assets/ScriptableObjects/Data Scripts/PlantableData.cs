using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plantable", menuName = "Inventory/PlantableData")]
public class PlantableData : ItemData
{
    public int growthTime; //in days
}