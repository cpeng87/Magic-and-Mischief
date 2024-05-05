using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : Item
{
    public SpellData spellData;
    private void Awake()
    {
        base.data = spellData;
    }
    public override bool Use()
    {
        return GameManager.instance.player.ps.CastSpell(spellData.spellManaCost);
    }
}
