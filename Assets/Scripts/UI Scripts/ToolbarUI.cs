using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    public InventoryUI inventoryUI;

    private void Update()
    {
        CheckAlphaNumericKeys();

        if (Input.GetMouseButtonDown(0))
        {
            UseItemSelectedInToolbar();
        }
    }

    public void UseItemSelectedInToolbar()
    {
        if (GameManager.instance.uiManager.PeekActiveMenu() != null)
        {
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
            return;
        }
        GameManager.instance.itemManager.UseItem(inventoryUI.GetInventory().slots[inventoryUI.selectorSlotID].itemName, inventoryUI.selectorSlotID);
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
}
