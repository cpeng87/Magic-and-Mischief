using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public int count;
        public int maxAllowed;
        public string itemName;
        public string itemDescription;

        public Sprite icon;

        public Slot()
        {
            itemName = "";
            count = 0;
            maxAllowed = 999;
        }
        public bool IsEmpty()
        {
            if (itemName == "" && count == 0)
            {
                return true;
            }
            return false;
        }
        public bool CanAddItem(string itemName)
        {
            if (this.itemName == itemName && count < maxAllowed)
            {
                return true;
            }
            return false;
        }
        public void AddItem(Item item)
        {
            this.itemName = item.data.itemName;
            this.itemDescription = item.data.description;
            this.icon = item.data.icon;
            count++;
            InventoryEventHandler.TriggerInventoryChangedEvent();
        }
        public void AddItem(string itemName, string description, Sprite icon, int maxAllowed, int quantity)
        {
            this.itemName = itemName;
            this.itemDescription = description;
            this.icon = icon;
            count += quantity;
            InventoryEventHandler.TriggerInventoryChangedEvent();
        }
        public void AddItem(string itemName, string description, Sprite icon, int maxAllowed)
        {
            this.itemName = itemName;
            this.itemDescription = description;
            this.icon = icon;
            count++;
            InventoryEventHandler.TriggerInventoryChangedEvent();
        }
        public void RemoveItem()
        {
            if(count > 0)
            {
                count--;
                if (count == 0)
                {
                    icon = null;
                    itemName = "";
                }
                InventoryEventHandler.TriggerInventoryChangedEvent();
            }
        }
        public void RemoveItem(int numToBeRemoved)
        {
            if(count > 0 && count >= numToBeRemoved)
            {
                count -= numToBeRemoved;
                if (count <= 0)
                {
                    icon = null;
                    itemName = "";
                }
                InventoryEventHandler.TriggerInventoryChangedEvent();
            }
        }
        public void ClearItem(){
            if (GameManager.instance.itemManager.GetItemByName(itemName) is KeyItem)
            {
                return;
            }
            icon = null;
            itemName = "";
            count = 0;
            InventoryEventHandler.TriggerInventoryChangedEvent();
        }
    }
    public List<Slot> slots = new List<Slot>();
    public Slot selectedSlot;
    // Start is called before the first frame update

    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public bool Add(Item item)
    {
        if (item == null)
        {
            Debug.Log("Item to add is null in Inventory");
            return false;
        }
        foreach(Slot slot in slots)
        {
            if (slot.itemName == item.data.itemName && slot.CanAddItem(item.data.itemName))
            {
                slot.AddItem(item);
                return true;
            }
        }

        foreach(Slot slot in slots)
        {
            if(slot.itemName == "")
            {
                slot.AddItem(item);
                return true;
            }
        }

        return false;
    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }
    public void Remove(int index, int numToBeRemoved)
    {
        if (slots[index].count >= numToBeRemoved)
        {
            for (int i = 0; i < numToBeRemoved; i++)
            {
                Remove(index);
            }
        }
    }

    public void Clear(int index)
    {
        slots[index].ClearItem();
    }

    public bool CheckInventoryForItemAndQuantity(string nameToFind, int quantity)
    {
        int counter = 0;
        foreach (Slot slot in slots)
        {
            if (slot.itemName == nameToFind)
            {
                counter += slot.count;
                if (counter >= quantity)
                {
                    return true;
                }
            }
        }
        if (counter >= quantity)
        {
            return true;
        }
        return false;
    }

    public void SubtractItemAndQuantity(string nameToFind, int quantity)
    {
        int counter = quantity;
        foreach (Slot slot in slots)
        {
            if (slot.itemName == nameToFind)
            {
                if (counter - slot.count <= 0)
                {
                    slot.RemoveItem(counter);
                    return;
                }
                else
                {
                    counter -= slot.count;
                    slot.RemoveItem(slot.count);
                }
            }
        }
    }
    public void MoveSlot(int fromIndex, int toIndex, Inventory toInventory)
    {
        Slot fromSlot = slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];

        if (toSlot.IsEmpty() || toSlot.CanAddItem(fromSlot.itemName))
        {
            toSlot.AddItem(fromSlot.itemName, fromSlot.itemDescription, fromSlot.icon, fromSlot.maxAllowed);
            fromSlot.RemoveItem();
        }
        // InventoryEventHandler.TriggerInventoryChangedEvent();
    }

    public void MoveSlot(int fromIndex, int toIndex, Inventory toInventory, int quantity)
    {
        Slot fromSlot = slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];

        if (quantity > fromSlot.count)
        {
            quantity = fromSlot.count;
        }

        if (toSlot.IsEmpty() || toSlot.CanAddItem(fromSlot.itemName))
        {
            toSlot.AddItem(fromSlot.itemName, fromSlot.itemDescription, fromSlot.icon, fromSlot.maxAllowed, quantity);
            fromSlot.RemoveItem(quantity);
        }
        // InventoryEventHandler.TriggerInventoryChangedEvent();
    }
    public void SelectSlot(int index)
    {
        if (slots != null && slots.Count > 0)
        {
            selectedSlot = slots[index];
            InventoryEventHandler.TriggerSelectedSlotChangedEvent();
        }
    }

    public void SetInventorySlots(List<Slot> slots)
    {
        this.slots = slots;
        InventoryEventHandler.TriggerInventoryChangedEvent();
    }
}
