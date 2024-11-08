using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KeyItem : Item
{
    // public KeyItemData keyItemData;
    [HideInInspector] public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public override bool Use()
    {
        return GameManager.instance.player.ti.UseItemOnTile(data.itemName);
    }
}
