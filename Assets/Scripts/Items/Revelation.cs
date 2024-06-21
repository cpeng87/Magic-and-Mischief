using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revelation : Spell
{
    public InvisibleMap invisibleMap;
    private float duration = 60f;
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
            Debug.Log(duration);
            GameManager.instance.player.bm.StartUniqueBuff(duration, data.icon, OnBuffBegin, OnBuffEnd);
        }
        return result;
    }

    private void OnBuffBegin()
    {
        if (invisibleMap == null)
        {
            FindInvisibleMap();
        } 
        invisibleMap.SetActive();
    }
    
    private void OnBuffEnd()
    {
        // invisibleMap.EndFade();
        invisibleMap.End();
    }
}
