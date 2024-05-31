using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop", menuName = "NPC/ShopData")]
public class ShopData : ScriptableObject
{
    public string shopName;
    public List<IngredientListing> sellableItems = new List<IngredientListing>();
}
