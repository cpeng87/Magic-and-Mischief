using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revelation : Spell
{
    public InvisibleMap invisibleMap;
    private float time = 5f;
    // Start is called before the first frame update
    private void FindInvisibleMap()
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
            bool buffResult = GameManager.instance.player.bm.StartUniqueBuff(time, data.icon, OnBuffEnd);
            if (invisibleMap == null)
            {
                FindInvisibleMap();
            } 
            invisibleMap.SetActiveAndFade(time);
        }
        return result;
    }
    
    private void OnBuffEnd()
    {
        invisibleMap.EndFade();
    }
}
