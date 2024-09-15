using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacter : MonoBehaviour
{
    public NPCData npcData;
    private float affectionLevel;

    public int GetAffectionLevel()
    {
        return (int) affectionLevel;
    }

    public void SetAffectionLevel(float newLevel)
    {
        affectionLevel = newLevel;
    }

    public bool GiveGift(Item item)
    {
        affectionLevel = affectionLevel + 1;
        return true;
        return false;
    }
}
