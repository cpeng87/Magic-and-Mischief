using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public string inventoryName;

    private Inventory inventory;

    public List<SlotUI> slots = new List<SlotUI>();
    public ToolbarUI toolbarUI;

    public GameObject selector;
    public int selectorSlotID;

    private int maxCol = 8;
    private int maxRow = 4;
    private int currX = 0;
    private int currY = 0;

    public TextMeshProUGUI itemNameTextbox;
    public TextMeshProUGUI itemDescriptionTextbox;

    public SlotUI selectedSlot;
    private int selectedCount;

    private void Start()
    {
        inventory = GameManager.instance.player.inventory.GetInventoryByName(inventoryName);
        SetupSlots();
        inventoryPanel.SetActive(false);
        selectorSlotID = 0;
        InventoryEventHandler.OnInventoryChanged += Refresh;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
            UpdateItemText(inventory.slots[(currY * maxCol) + currX]);
        }
        if (GameManager.instance.PeekActiveMenu() != this.gameObject)
        {
            return;
        }
        if (inventoryPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveSelector(currX + 1, currY);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveSelector(currX - 1, currY);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveSelector(currX, currY - 1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveSelector(currX, currY + 1);
            }
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                Remove();
            } 
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                Clear();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                Move(slots[selectorSlotID]);
            }
        }
    }

    public void ToggleInventory()
    {
        if (inventoryPanel != null)
        {
            if (!inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(true);
                Time.timeScale = 0f;
                GameManager.instance.PushActiveMenu(this.gameObject);
                Refresh();
            }
            else
            {   
                inventoryPanel.SetActive(false);
                GameManager.instance.PopActiveMenu();
                if (GameManager.instance.activeMenuCount == 0)
                {
                    Time.timeScale = 1f;
                }
            }
        }
    }

    void Refresh()
    {
        if (slots.Count == inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
            RefreshToolbar();
            SetAllMoveBorderInactive();
        }
    }

    public void Remove()
    {
        Item itemToDrop = GameManager.instance.itemManager.GetItemByName(inventory.slots[(currY * maxCol) + currX].itemName);
        
        if (itemToDrop != null) {
            GameManager.instance.player.DropItem(itemToDrop);
            inventory.Remove((currY * maxCol) + currX);
            Refresh();

            //updates in case there are no items left
            UpdateItemText(inventory.slots[(currY * maxCol) + currX]);
        }
    }

    public void Clear()
    {
        inventory.Clear((currY * maxCol) + currX);
        Refresh();
        UpdateItemText(inventory.slots[(currY * maxCol) + currX]);
    }

    public void MoveSelector(int x, int y)
    {
        if (x < 0 || y < 0 || x >= maxCol || y >= maxRow)
        {
            return;
        }
        currX = x;
        currY = y;

        selector.transform.position = slots[(y * maxCol) + x].transform.position;

        UpdateItemText(inventory.slots[(y * maxCol) + x]);
        selectorSlotID = slots[(y * maxCol) + x].slotID;
    }

    private void UpdateItemText(Inventory.Slot slot)
    {
        if (itemDescriptionTextbox == null || itemNameTextbox == null)
        {
            return;
        }
        if (slot.itemName == "") 
        {
            itemNameTextbox.text = "No Item Selected";
            itemDescriptionTextbox.text = "";
        }
        else
        {
            itemNameTextbox.text = slot.itemName;
            itemDescriptionTextbox.text = slot.itemDescription;
        }
    }
    void SetupSlots()
    {
        int counter = 0;
        foreach (SlotUI slot in slots)
        {
            slot.slotID = counter;
            counter++;
            slot.inventory = inventory;
        }
    }

    public void SlotDrop(SlotUI slot)
    {
        selectedSlot.inventory.MoveSlot(selectedSlot.slotID, slot.slotID, slot.inventory, selectedCount);
    }

    public void Move(SlotUI slot) 
    {
        if (selectedSlot != null)
        {
            if (slot.CheckItemIcon(selectedSlot))
            {
                selectedCount++;
                slot.SetMoveBorderActive();
            }
            else {
                SlotDrop(slot);
                Refresh();
                selectedSlot = null;
                selectedCount = 0;
                SetAllMoveBorderInactive();
            }
        }
        else
        {
            selectedSlot = slot;
            slot.SetMoveBorderActive();
            selectedCount = 1;
        }
    }

    private void SetAllMoveBorderInactive() {
        foreach (SlotUI slot in slots)
        {
            slot.SetMoveBorderInactive();
        }
    }

    public void RefreshToolbar()
    {
        toolbarUI.Refresh();
    }
}
