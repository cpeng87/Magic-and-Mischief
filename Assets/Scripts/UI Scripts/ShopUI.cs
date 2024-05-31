using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopUI : MonoBehaviour
{
    public GameObject display;
    public ShopData shopData;
    public List<ShopSlotUI> slots = new List<ShopSlotUI>();
    public GameObject slotParent;
    public GameObject inventorySlotParent;
    public GameObject slotUIPrefab;

    void Start()
    {
        display.SetActive(false);
        SetupInventorySlots();
    }

    public void ToggleUI()
    {
        if (!display.activeSelf)
        {
            SetupSlots();
            SelectSlot(0);
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

    public void SelectSlot(int slotID)
    {
        SetAllBorderInactive();
        slots[slotID].SetMoveBorderActive();
    }

    public void PurchaseItem(int slotID)
    {
        SelectSlot(slotID);
        Debug.Log("Purchasing item at: " + slotID);
        if (GameManager.instance.player.currency >= slots[slotID].price)
        {
            Item item = GameManager.instance.itemManager.GetItemByName(slots[slotID].itemName);
            GameManager.instance.player.inventory.Add("Backpack", item);
            GameManager.instance.player.SubtractCurrency(slots[slotID].price);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void SetupSlots()
    {
        ClearSlots();
        int counter = 0;
        foreach (IngredientListing item in shopData.sellableItems)
        {
            GameObject newSlot = Instantiate(slotUIPrefab, slotParent.transform);
            ShopSlotUI newShopSlotUI = newSlot.GetComponent<ShopSlotUI>();
            newShopSlotUI.shopSlotID = counter;

            // Add event trigger for left-click (purchase)
            AddEventTrigger(newSlot, EventTriggerType.PointerClick, (eventData) => { PurchaseItem(newShopSlotUI.shopSlotID); });

            // // Add event trigger for right-click (sell)
            // AddEventTrigger(newSlot, EventTriggerType.PointerClick, (eventData) => { if (((PointerEventData)eventData).button == PointerEventData.InputButton.Right) SellItem(newShopSlotUI.shopSlotID); });

            newShopSlotUI.SetItemValues(item.listingData.name, item.quantity, item.listingData.icon);
            slots.Add(newShopSlotUI);
            counter++;
        }
    }

    public void SetupInventorySlots()
    {
        foreach (Transform slot in inventorySlotParent.transform)
        {
            SlotUI inventorySlotUI = slot.GetComponent<SlotUI>();
            AddEventTrigger(slot.gameObject, EventTriggerType.PointerClick, (eventData) => { if (((PointerEventData)eventData).button == PointerEventData.InputButton.Right) SellItem(inventorySlotUI.slotID); });
        }
    }

    private void AddEventTrigger(GameObject gameObject, EventTriggerType eventType, System.Action<BaseEventData> action)
    {
        EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
        if (trigger == null) trigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(action));
        trigger.triggers.Add(entry);
    }

    private void ClearSlots()
    {
        if (slotParent == null)
        {
            return;
        }

        // Clear the slots list to remove references to destroyed GameObjects
        slots.Clear();

        foreach (Transform slot in slotParent.transform)
        {
            Destroy(slot.gameObject);
        }
    }

    private void SellItem(int slotID)
    {
        if (GameManager.instance.player.inventory.GetInventory("Backpack").slots[slotID].itemName == "")
        {
            return;
        }
        Item soldItem = GameManager.instance.itemManager.GetItemByName(GameManager.instance.player.inventory.GetInventory("Backpack").slots[slotID].itemName);
        if (soldItem.data is SellableItemData)
        {
            GameManager.instance.player.AddCurrency(((SellableItemData) soldItem.data).sellPrice);
            GameManager.instance.player.inventory.GetInventory("Backpack").Remove(slotID);
        }
    }

    private void SetAllBorderInactive()
    {
        foreach (ShopSlotUI slot in slots)
        {
            slot.SetMoveBorderInactive();
        }
    }

    public void SetShopData(ShopData newData)
    {
        shopData = newData;
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;

// public class ShopUI : MonoBehaviour
// {
//     public GameObject display;
//     public ShopData shopData;
//     public List<ShopSlotUI> slots = new List<ShopSlotUI>();
//     public GameObject slotParent;
//     public GameObject slotUIPrefab;
//     // Start is called before the first frame update
//     void Start()
//     {
//         display.SetActive(false);
//     }

//     // Update is called once per frame
//     public void ToggleUI()
//     {

//         if (!display.activeSelf)
//         {
//             SetupSlots();
//             SelectSlot(0);
//             display.SetActive(true);
//             Time.timeScale = 0f;
//             GameManager.instance.PushActiveMenu(this.gameObject);
//         }
//         else
//         {   
//             display.SetActive(false);
//             GameManager.instance.PopActiveMenu();
//             if (GameManager.instance.activeMenuCount == 0)
//             {
//                 Time.timeScale = 1f;
//             }
            
//         }
//     }

//     public void SelectSlot(int slotID)
//     {
//         SetAllBorderInactive();
//         slots[slotID].SetMoveBorderActive();
//     }
//     public void PurchaseItem(int slotID)
//     {
//         SelectSlot(slotID);
//         Debug.Log("Purchasing item at: " + slotID);
//         if (GameManager.instance.player.currency >= slots[slotID].price)
//         {
//             Item item = GameManager.instance.itemManager.GetItemByName(slots[slotID].name);
//             GameManager.instance.player.inventory.Add("Backpack", item);
//             GameManager.instance.player.SubtractCurrency(slots[slotID].price);
//         }
//         else
//         {
//             Debug.Log("Not enough money!");
//         }
//         // GameManager.instance.player.inventory.
//     }
//     public void SetupSlots()
//     {
//         ClearSlots();
//         int counter = 0;
//         foreach (IngredientListing item in shopData.sellableItems)
//         {
//             GameObject newSlot = Instantiate(slotUIPrefab, slotParent.transform);
//             ShopSlotUI newShopSlotUI = newSlot.GetComponent<ShopSlotUI>();
//             newShopSlotUI.shopSlotID = counter;

//             //adding event trigger for clicking on the shop listing
//             EventTrigger eventTrigger = newSlot.AddComponent<EventTrigger>();
//             EventTrigger.Entry entry = new EventTrigger.Entry
//             {
//                 eventID = EventTriggerType.PointerClick
//             };
//             entry.callback.AddListener((eventData) => { PurchaseItem(newShopSlotUI.shopSlotID); });

//             eventTrigger.triggers.Add(entry);
//             newShopSlotUI.SetItemValues(item.listingData.name, item.quantity, item.listingData.icon);

//             slots.Add(newShopSlotUI);
//             counter++;
//         }
//     }
//     private void ClearSlots()
//     {
//         if (slotParent == null)
//         {
//             return;
//         }

//         // Clear the slots list to remove references to destroyed GameObjects
//         slots.Clear();

//         foreach (Transform slot in slotParent.transform)
//         {
//             Destroy(slot.gameObject);
//         }
//     }
//     private void SellItem(SlotUI slotui)
//     {
//         // GameManager.instance.player.inventory.GetInventoryByName("Backpack").slots[slotID].name;
//         Item soldItem = GameManager.instance.itemManager.GetItemByName(GameManager.instance.player.inventory.GetInventory("Backpack").slots[slotui.slotID].itemName);
//         if (soldItem.data is SellableItemData)
//         {
//             // GameManager.instance.player.AddCurrency(soldItem.data.sellPrice);
//             slotui.inventory.Remove(slotui.slotID);
//         }
//     }
//     private void SetAllBorderInactive()
//     {
//         foreach (ShopSlotUI slot in slots)
//         {
//             slot.SetMoveBorderInactive();
//         }
//     }
//     public void SetShopData(ShopData newData)
//     {
//         shopData = newData;
//     }
// }
