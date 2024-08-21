using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackUI : Toggleable
{
    public InventoryUI inventoryUI;
    
    // Start is called before the first frame update
    void Awake()
    {
        inventoryUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleUI();
        }
        
        if (GameManager.instance.uiManager.PeekActiveMenu() != this)
        {
            return;
        }
    }

    // public override void ToggleUI()
    // {
    //     if (!inventoryUI.gameObject.activeSelf)
    //     {
    //         inventoryUI.gameObject.SetActive(true);
    //         UnityEngine.Time.timeScale = 0f;
    //         GameManager.instance.PushActiveMenu(this.gameObject);
    //     }
    //     else
    //     {   
    //         inventoryUI.gameObject.SetActive(false);
    //         GameManager.instance.uiManager.PopActiveMenu();
    //         if (GameManager.instance.activeMenuCount == 0)
    //         {
    //             UnityEngine.Time.timeScale = 1f;
    //         }
    //     }
    // }
}
