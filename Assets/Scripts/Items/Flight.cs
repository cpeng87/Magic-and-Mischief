using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : Spell
{
    private float time = 30f;

    // Update is called once per frame
    public override bool Use()
    {
        // return GameManager.instance.player.ps.CastSpell(((SpellData) data).spellManaCost);
        bool result = GameManager.instance.player.ps.CastSpell((SpellData) data);
        if (result)
        {
            bool buffResult = GameManager.instance.player.bm.StartFlightBuff(3f, time, OnBuffEnd);
            if (buffResult)
            {
                GameManager.instance.player.pa.AnimateFlight();
            }
        }
        return result;
    }

    private void OnBuffEnd()
    {
        GameManager.instance.player.pa.StopAnimateFlight();
    }
}
