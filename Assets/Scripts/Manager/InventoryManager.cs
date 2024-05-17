using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();

    [Header("Backpack")]
    public Inventory backpack;
    public int backpackSlotCount;
    public Dictionary<Vector3Int, Inventory> chests = new Dictionary<Vector3Int, Inventory>();

    private void Awake()
    {
        backpack = new Inventory(backpackSlotCount);

        inventoryByName.Add("Backpack", backpack);
    }

    public void Add(string inventoryName, Item item)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            inventoryByName[inventoryName].Add(item);
        }
    }

    public Inventory GetInventoryByName(string inventoryName)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            return inventoryByName[inventoryName];
        }

        return null;
    }
    public void AddChest(Vector3Int position)
    {
        Inventory chest = new Inventory(24);
        chests.Add(position, chest);
        // chests.Add(chest);
    }
}
