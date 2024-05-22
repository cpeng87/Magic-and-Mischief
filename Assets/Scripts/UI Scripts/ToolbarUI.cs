using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    // public GameObject slotParent;
    // public GameObject selector;
    // private Inventory inventory;
    // public string inventoryName;
    public InventoryUI inventoryUI;
    // public SlotUI selectedSlot;

    private void Update()
    {
        CheckAlphaNumericKeys();
    }

    public void UseItemSelectedInToolbar()
    {
        if (GameManager.instance.PeekActiveMenu() != null)
        {
            // Debug.Log("another menu active");
            return;
        }
        GameManager.instance.player.ti.CheckHarvestable();
        if (inventoryUI.GetInventory().slots[inventoryUI.selectorSlotID] == null)
        {
            Debug.Log("the item in selected slot is null");
            return;
        }
        else if (inventoryUI.GetInventory().slots[inventoryUI.selectorSlotID].itemName == "")
        {
            Debug.Log("the item in selected slot itemname is blank");
            return;
        }
        bool result = GameManager.instance.itemManager.GetItemByName(inventoryUI.GetInventory().slots[inventoryUI.selectorSlotID].itemName).Use();
        GameManager.instance.player.ti.UseItemOnTile();

        //true if item is consumed on usage
        if (result)
        {
            inventoryUI.slots[inventoryUI.selectorSlotID].inventory.Remove(inventoryUI.selectorSlotID);
            InventoryEventHandler.TriggerSelectedSlotChangedEvent();
        }
    }

    private void CheckAlphaNumericKeys()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventoryUI.SlotSelect(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventoryUI.SlotSelect(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventoryUI.SlotSelect(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventoryUI.SlotSelect(3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            inventoryUI.SlotSelect(4);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            inventoryUI.SlotSelect(5);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            inventoryUI.SlotSelect(6);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            inventoryUI.SlotSelect(7);
        }
    }

    // private void MoveSelector(int index)
    // {
    //     selector.transform.position = toolbarSlots[index].transform.position;
    // }

    // public void Refresh()
    // {
    //     for (int i = 0; i < toolbarSlots.Count; i++)
    //     {
    //         if (inventory.slots[i].itemName != "")
    //         {
    //             toolbarSlots[i].SetItem(inventory.slots[i]);
    //         }
    //         else
    //         {
    //             toolbarSlots[i].SetEmpty();
    //         }
    //     }
    // }
    // void SetupSlots()
    // {
    //     int counter = 0;
    //     foreach (SlotUI slot in toolbarSlots)
    //     {
    //         slot.slotID = counter;
    //         counter++;
    //         slot.inventory = inventory;
    //     }
    // }
}
