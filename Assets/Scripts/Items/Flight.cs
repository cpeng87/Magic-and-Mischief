using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : Spell
{
    private float time = 60f;

    // Update is called once per frame
    public override bool Use()
    {
        bool result = GameManager.instance.player.ps.CastSpell((SpellData) data);
        if (result)
        {
            GameManager.instance.player.bm.StartSpeedBuff(3f, time, data.icon, OnBuffBegin, OnBuffEnd);
        }
        return result;
    }

    private void OnBuffEnd()
    {
        GameManager.instance.player.bm.SetIsFlying(false);
        GameManager.instance.player.pa.StopAnimateFlight();
        BuffEventHandler.TriggerFlightChangedEvent();
    }

    private void OnBuffBegin()
    {
        GameManager.instance.player.bm.SetIsFlying(true);
        GameManager.instance.player.pa.AnimateFlight();
        BuffEventHandler.TriggerFlightChangedEvent();
    }
}
