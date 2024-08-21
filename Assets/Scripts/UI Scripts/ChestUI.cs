using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUI : Toggleable
{
    public Vector3Int pos;
    public InventoryUI inventoryUI;
    public InventoryUI chestUI;
    // public GameObject display;

    void Start()
    {
        toggledDisplay.SetActive(false);
    }

    public void SetPosition(Vector3Int newPosition)
    {
        pos = newPosition;
    }

    public override void Setup()
    {
        chestUI.SetInventory(pos);
    }
}
