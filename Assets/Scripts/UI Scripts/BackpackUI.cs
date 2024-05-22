using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackUI : MonoBehaviour
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
            ToggleInventory();
        }
        
        if (GameManager.instance.PeekActiveMenu() != this.gameObject)
        {
            return;
        }
    }

    public void ToggleInventory()
    {
        if (!inventoryUI.gameObject.activeSelf)
        {
            inventoryUI.gameObject.SetActive(true);
            Time.timeScale = 0f;
            GameManager.instance.PushActiveMenu(this.gameObject);
        }
        else
        {   
            inventoryUI.gameObject.SetActive(false);
            GameManager.instance.PopActiveMenu();
            if (GameManager.instance.activeMenuCount == 0)
            {
                Time.timeScale = 1f;
            }
        }
    }
}
