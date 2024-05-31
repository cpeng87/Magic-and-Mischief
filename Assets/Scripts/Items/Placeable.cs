using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : Item
{
    // public PlaceableData placeableData;
    // private void Awake()
    // {
    //     base.data = spellData;
    // }
    public override bool Use()
    {
        //goes to the right and up for the selector
        return GameManager.instance.player.ti.PlaceItem(((PlaceableData) data).placedItem, ((PlaceableData) data).size);
    }
}
