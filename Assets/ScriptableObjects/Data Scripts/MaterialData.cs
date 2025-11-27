using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material", menuName = "Inventory/MaterialData")]
public class MaterialData : SellableItemData
{
    public float volatility;  // stability of circle vs volatility of ingredients

    //changeable attributes
    public float fire;
    public float wind;
    public float water;
    public float earth;
}
