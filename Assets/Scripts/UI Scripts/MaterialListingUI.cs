using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialListingUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemQuantityText;
    public Image image;

    public void SetMaterialListing(string name, int quantity, Sprite icon)
    {
        itemNameText.text = name;
        itemQuantityText.text = quantity.ToString();
        image.sprite = icon;
    }
}
