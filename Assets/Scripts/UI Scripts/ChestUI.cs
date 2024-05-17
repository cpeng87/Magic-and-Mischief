using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUI : InteractUI
{
    public Inventory chest;
    public GameObject chestUI;

    // Start is called before the first frame update
    void Awake()
    {
        chestUI.SetActive(false);
    }

    public override void ToggleUI()
    {
        if (!chestUI.activeSelf)
        {
            chestUI.SetActive(true);
            Time.timeScale = 0f;
            GameManager.instance.PushActiveMenu(this.gameObject);
            LoadChest();
        }
        else
        {   
            chestUI.SetActive(false);
            GameManager.instance.PopActiveMenu();
            if (GameManager.instance.activeMenuCount == 0)
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void LoadChest()
    {
        Inventory currChest = GameManager.instance.player.inventory.chests[position];
    }
}
