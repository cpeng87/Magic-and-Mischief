using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : Item
{
    public override bool Use()
    {
        // return GameManager.instance.player.ps.CastSpell(((SpellData) data).spellManaCost);
        return GameManager.instance.player.ps.CastSpell((SpellData) data);
    }
}
