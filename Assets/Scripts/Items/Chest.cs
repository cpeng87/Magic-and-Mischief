using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Placeable
{
    private void Awake()
    {
        
    }
    public override bool Use()
    {
        //goes to the right and up for the selector
        return GameManager.instance.player.ti.PlaceItem(placeableData.placedItem, placeableData.size);
    }
}
