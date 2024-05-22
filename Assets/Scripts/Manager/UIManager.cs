using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Dictionary<string, InventoryUI> inventoryUIByName = new Dictionary<string, InventoryUI>();
    public List<InventoryUI> inventoryUIs;
    public SlotUI draggedSlot;
    public Image draggedIcon;
    private bool dragSingle;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else
        {
            dragSingle = false;
        }
    }

    public InventoryUI GetInventoryUI(string inventoryName)
    {
        if (inventoryUIByName.ContainsKey(inventoryName))
        {
            return inventoryUIByName[inventoryName];
        }

        return null;
    }
    void Initialize()
    {
        InventoryUI[] inventoryUIsInScene = FindObjectsOfType<InventoryUI>();
        // Loop through each instance and perform actions
        foreach (InventoryUI ui in inventoryUIsInScene)
        {
            inventoryUIs.Add(ui);
        }

        foreach(InventoryUI ui in inventoryUIs)
        {
            if (!inventoryUIByName.ContainsKey(ui.inventoryName))
            {
                inventoryUIByName.Add(ui.inventoryName, ui);
            }
        }
    }
}
