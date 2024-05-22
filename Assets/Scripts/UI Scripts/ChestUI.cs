using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUI : InteractUI
{
    public InventoryUI inventoryUI;
    public InventoryUI chestUI;
    public GameObject display;


    void Start()
    {
        display.SetActive(false);
    }

    // Update is called once per frame
    public override void ToggleUI()
    {
        if (!display.activeSelf)
        {
            // display.SetActive(true);
            LoadChest();
            // if (chestUI.inventory == null)
            // {
            //     return;
            // }
            display.SetActive(true);
            Time.timeScale = 0f;
            GameManager.instance.PushActiveMenu(this.gameObject);
        }
        else
        {   
            display.SetActive(false);
            GameManager.instance.PopActiveMenu();
            if (GameManager.instance.activeMenuCount == 0)
            {
                Time.timeScale = 1f;
            }
            
        }
    }

    private void LoadChest()
    {
        chestUI.SetInventory(pos);
    }
}
