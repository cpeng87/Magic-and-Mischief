using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantable : Item
{
    public override bool Use()
    {
        return GameManager.instance.player.ti.UseItemOnTile(data.itemName);
    }
}
