using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{
    public int slotID;
    public Inventory inventory;

    public Image itemIcon;
    public TextMeshProUGUI quantityText;

    public Image moveBorder;

    public void SetItem(Inventory.Slot slot)
    {
        itemIcon.sprite = slot.icon;
        itemIcon.color = new Color(1,1,1,1);
        quantityText.text = slot.count.ToString();
    }
    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1,1,1,0);
        quantityText.text = "";
    }
    // public bool CheckItemIcon(SlotUI slotui1)
    // {
    //     if (slotui1.itemIcon == itemIcon)
    //     {
    //         Debug.Log("icon same");
    //         return true;
    //     }
    //     return false;
    // }
    public void SetMoveBorderActive()
    {
        moveBorder.enabled = true;
    }
    public void SetMoveBorderInactive()
    {
        if (moveBorder == null)
        {
            return;
        }
        moveBorder.enabled = false;
    }
}
