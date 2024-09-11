using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] items;
    private Dictionary<string, Item> nameToItemsDict = 
        new Dictionary<string, Item>();
    private Dictionary<string, ItemData> nameToItemDataDict = 
        new Dictionary<string, ItemData>();

    private void Awake()
    {
        foreach(Item item in items)
        {
            AddItem(item);
        }
    }

    private void AddItem(Item item)
    {
        if (item == null || item.data == null)
        {
            return;
        }

        if (!nameToItemsDict.ContainsKey(item.data.itemName))
        {
            nameToItemsDict.Add(item.data.itemName, item);
            nameToItemDataDict.Add(item.data.itemName, item.data);
        }
    }

    public Item GetItemByName(string name)
    {
        if (nameToItemsDict.ContainsKey(name))
        {
            return nameToItemsDict[name];
        }
        return null;
    }
    public ItemData GetItemDataByName(string name)
    {
        if (nameToItemDataDict.ContainsKey(name))
        {
            return nameToItemDataDict[name];
        }
        return null;
    }

    public void UseItem(string name, int slotID)
    {
        bool result = GameManager.instance.itemManager.GetItemByName(name).Use();
        //change to pass in the name instead?
        GameManager.instance.player.ti.UseItemOnTile();

        if (result)
        {
            GameManager.instance.player.inventory.GetInventory("Toolbar").Remove(slotID);
            InventoryEventHandler.TriggerSelectedSlotChangedEvent();
        }
    }
}
