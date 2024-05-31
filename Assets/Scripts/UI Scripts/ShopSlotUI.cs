using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlotUI : MonoBehaviour
{
    public Image moveBorder;
    public int shopSlotID;
    public string itemName;
    public int price;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public Image image;

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
    public void SetItemValues(string newName, int newPrice, Sprite newImage)
    {
        itemName = newName;
        nameText.text = newName;
        price = newPrice;
        priceText.text = newPrice.ToString();
        image.sprite = newImage;
    }
}
