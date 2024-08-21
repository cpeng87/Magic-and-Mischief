using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public string inventoryName;

    public Inventory inventory;

    public GameObject slotParent;
    public List<SlotUI> slots = new List<SlotUI>();

    public int selectorSlotID = 0;

    public TextMeshProUGUI itemNameTextbox;
    public TextMeshProUGUI itemDescriptionTextbox;

    private Canvas canvas;

    private UIManager uiManager;
    private bool dragSingle;

    public Player player;

    private void Awake()
    {
        canvas = FindCanvas();
    }

    public Canvas FindCanvas()
    {
        Transform current = this.gameObject.transform.parent;

        while (current != null)
        {
            Canvas canvas = current.GetComponent<Canvas>();
            if (canvas != null)
            {
                return canvas;
            }
            current = current.parent;
        }

        return null;
    }

    public void Start()
    {
        player = GameManager.instance.player;
        uiManager = GameManager.instance.uiManager;
        inventory = GameManager.instance.player.inventory.GetInventory(inventoryName);

        SetupSlots();
        Refresh();
        SetAllMoveBorderInactive();

        InventoryEventHandler.OnInventoryChanged += Refresh;
    }

    private void OnEnable()
    {
        Refresh();
        SetAllMoveBorderInactive();
        // if (inventory != null && slots.Count > 0)
        // {
        //     SlotSelect(slots[0]);
        // }
    }

    public void SetInventory(Vector3Int pos)
    {
        inventory = player.inventory.GetInventoryChest(pos);
        SetupSlotInventory();
        Refresh();
    }

    public void SetInventory(string name)
    {
        if (player == null)
        {
            Debug.Log("player is null");
        }
        inventory = GameManager.instance.player.inventory.GetInventory(name);
        SetupSlotInventory();
        Refresh();
    }

    public void Refresh()
    {
        if (inventory == null)
        {
            return;
        }
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
        if (selectorSlotID >= 0 && selectorSlotID < slots.Count)
        {
            UpdateItemText(slots[selectorSlotID]);
        }
    }

    public void Remove()
    {
        if (uiManager.draggedSlot == null)
        {
            return;
        }
        Item itemToDrop = GameManager.instance.itemManager.GetItemByName(inventory.slots[uiManager.draggedSlot.slotID].itemName);
        
        if (itemToDrop != null) {
            if (dragSingle)
            {
                player.DropItem(itemToDrop);
                inventory.Remove(uiManager.draggedSlot.slotID);
            }
            else
            {
                player.DropItem(itemToDrop, inventory.slots[uiManager.draggedSlot.slotID].count);
                inventory.Remove(uiManager.draggedSlot.slotID, inventory.slots[uiManager.draggedSlot.slotID].count);
            }
            Refresh();
        }
        uiManager.draggedSlot = null;
    }

    private void UpdateItemText(SlotUI slotUi)
    {
        if (itemDescriptionTextbox == null || itemNameTextbox == null)
        {
            return;
        }
        else if (slotUi == null)
        {
            itemNameTextbox.text = "No Item Selected";
            itemDescriptionTextbox.text = "";
            return;
        }

        Inventory.Slot slot = inventory.slots[slotUi.slotID];
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
        foreach (Transform slot in slotParent.transform)
        {
            slots.Add(slot.gameObject.GetComponent<SlotUI>());
        }

        int counter = 0;
        foreach (SlotUI slot in slots)
        {
            slot.slotID = counter;
            counter++;
        }
        if (inventory != null)
        {
            SetupSlotInventory();
        }
    }

    private void SetupSlotInventory()
    {
        foreach (SlotUI slot in slots)
        {
            slot.inventory = inventory;
        }
    }

    public void SlotDrop(SlotUI slot)
    {
        if (uiManager.dragSingle)
        {
            uiManager.draggedSlot.inventory.MoveSlot(uiManager.draggedSlot.slotID, slot.slotID, slot.inventory, 1);
        }
        else
        {
            uiManager.draggedSlot.inventory.MoveSlot(uiManager.draggedSlot.slotID, slot.slotID, slot.inventory, uiManager.draggedSlot.inventory.slots[uiManager.draggedSlot.slotID].count);
        }
    }

    private void SetAllMoveBorderInactive() 
    {
        foreach (SlotUI slot in slots)
        {
            slot.SetMoveBorderInactive();
        }
        if (itemNameTextbox != null)
        {
            itemNameTextbox.text = "No Item Selected";
        }
        if (itemDescriptionTextbox != null)
        {
            itemDescriptionTextbox.text = "";
        }
    }

    public void SetOneMoveBorderActive(int index)
    {
        slots[index].SetMoveBorderActive();
    }

    public void BeginDrag(SlotUI slot)
    {
        uiManager.draggedSlot = slot;
        uiManager.draggedIcon = Instantiate(uiManager.draggedSlot.itemIcon);
        uiManager.draggedIcon.transform.SetParent(canvas.transform);
        uiManager.draggedIcon.raycastTarget = false;
        uiManager.draggedIcon.rectTransform.sizeDelta = new Vector2(75, 75);
        MoveToMousePosition(uiManager.draggedIcon.gameObject);
    }

    public void SlotDrag()
    {
        MoveToMousePosition(uiManager.draggedIcon.gameObject);
    }

    public void EndDrag()
    {
        Destroy(uiManager.draggedIcon.gameObject);
        uiManager.draggedIcon = null;
        uiManager.draggedSlot = null;
    }

    private void MoveToMousePosition(GameObject toMove)
    {
        if (canvas != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out position);

            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }

    public void SlotSelect(SlotUI slot)
    {
        int index = slot.slotID;
        SetAllMoveBorderInactive();
        slots[index].SetMoveBorderActive();
        UpdateItemText(slots[index]);
        selectorSlotID = index;
        inventory.SelectSlot(index);
    }

    public void SlotSelect(int index)
    {
        SetAllMoveBorderInactive();
        slots[index].SetMoveBorderActive();
        UpdateItemText(slots[index]);
        selectorSlotID = index;
        inventory.SelectSlot(index);
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    private void OnDestroy()
    {
        InventoryEventHandler.OnInventoryChanged -= Refresh;
    }
}
