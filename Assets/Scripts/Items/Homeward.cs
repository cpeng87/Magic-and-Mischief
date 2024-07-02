using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homeward : Spell
{
    private string homeSpawn = "Home Circle";
    private string sceneToLoad = "Tower";

    public override bool Use()
    {
        // return GameManager.instance.player.ps.CastSpell(((SpellData) data).spellManaCost);
        bool result = GameManager.instance.player.ps.CastSpell((SpellData) data);
        if (result)
        {
            GameManager.instance.sceneSwapManager.SceneSwap(sceneToLoad, homeSpawn);
        }
        return result;
    }
}