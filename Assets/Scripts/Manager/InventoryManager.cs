using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();
    public Dictionary<Vector3Int, Inventory> chests;
    public GameObject chestPrefab;

    [Header("Backpack")]
    public Inventory backpack;
    public int backpackSlotCount;

    [Header("Toolbar")]
    public Inventory toolbar;
    public int toolbarSlotCount;

    private void Awake()
    {
        Inventory backpack = new Inventory(backpackSlotCount);
        Inventory toolbar = new Inventory(toolbarSlotCount);

        if (chests == null)
        {
            chests = new Dictionary<Vector3Int, Inventory>();
        }

        inventoryByName.Add("Backpack", backpack);
        inventoryByName.Add("Toolbar", toolbar);
    }

    public bool Add(string inventoryName, Item item)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            return inventoryByName[inventoryName].Add(item);
        }

        return false;
    }

    public Inventory GetInventory(string inventoryName)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            return inventoryByName[inventoryName];
        }
        return null;
    }
    public Inventory GetInventoryChest(Vector3Int pos)
    {
        if (chests.ContainsKey(pos))
        {
            return chests[pos];
        }
        return null;
    }

    public void SetInventoryByName(string name, Inventory newInventory)
    {
        if (inventoryByName.ContainsKey(name))
        {
            inventoryByName[name] = newInventory;
        }
    }

    public void AddChest(Vector3Int position)
    {
        Inventory chest = new Inventory(24);
        chests.Add(position, chest);
    }

    public void LoadChests()
    {
        foreach(Vector3Int pos in chests.Keys)
        {
            Vector3 position = new Vector3(pos.x, pos.y, pos.z);
            GameObject newChest = Instantiate(chestPrefab, position, Quaternion.identity);
        }
    }

    public Dictionary<Vector3Int, Inventory> GetChests()
    {
        return chests;
    }
}
