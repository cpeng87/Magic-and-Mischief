using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpellType
{
    Projectile,
    AOE,
    Buff
}

[CreateAssetMenu(fileName = "New Spell", menuName = "Inventory/SpellData")]
public class SpellData : ItemData
{
    public int spellDamage;
    public int spellManaCost;
    public List<IngredientListing> ingredients = new List<IngredientListing>();
    public SpellType spellType;
}
