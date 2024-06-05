using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revelation : Spell
{
    public InvisibleMap invisibleMap;
    private float time = 5f;
    // Start is called before the first frame update
    private void FindInvisibileMap()
    {
        invisibleMap = FindObjectOfType<InvisibleMap>();
    }

    // Update is called once per frame
    public override bool Use()
    {
        // return GameManager.instance.player.ps.CastSpell(((SpellData) data).spellManaCost);
        bool result = GameManager.instance.player.ps.CastSpell((SpellData) data);
        if (result)
        {
            if (invisibleMap == null)
            {
                FindInvisibileMap();
            } 
            invisibleMap.SetActiveAndFade(time);
        }
        return result;
    }
}
