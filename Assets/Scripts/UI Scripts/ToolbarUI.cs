using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    [SerializeField] private List<SlotUI> toolbarSlots = new List<SlotUI>();
    public GameObject selector;
    public Inventory inventory;
    public string inventoryName;

    private void Start()
    {
        inventory = GameManager.instance.player.inventory.GetInventoryByName(inventoryName);
        Refresh();
        SetupSlots();
    }
    private void Update()
    {
        CheckAlphaNumericKeys();
    }
    public void SelectSlot(int index)
    {
        if (toolbarSlots.Count == 8)
        {
            SlotUI selectedSlot = toolbarSlots[index];
            MoveSelector(index);
            if (selectedSlot.inventory.slots[selectedSlot.slotID].IsEmpty())
            {
                inventory.SelectSlot(selectedSlot.slotID);
                return;
            }
            bool result = GameManager.instance.itemManager.GetItemByName(inventory.slots[selectedSlot.slotID].itemName).Use();
            if (result)
            {
                selectedSlot.inventory.Remove(selectedSlot.slotID);
            }
            inventory.SelectSlot(selectedSlot.slotID);
        }
    }

    private void CheckAlphaNumericKeys()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSlot(3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectSlot(4);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectSlot(5);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectSlot(6);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectSlot(7);
        }
    }

    private void MoveSelector(int index)
    {
        selector.transform.position = toolbarSlots[index].transform.position;
    }

    public void Refresh()
    {
        for (int i = 0; i < toolbarSlots.Count; i++)
        {
            if (inventory.slots[i].itemName != "")
            {
                toolbarSlots[i].SetItem(inventory.slots[i]);
            }
            else
            {
                toolbarSlots[i].SetEmpty();
            }
        }
    }
    void SetupSlots()
    {
        int counter = 0;
        foreach (SlotUI slot in toolbarSlots)
        {
            slot.slotID = counter;
            counter++;
            slot.inventory = inventory;
        }
    }
}
