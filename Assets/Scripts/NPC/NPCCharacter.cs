using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacter : MonoBehaviour
{
    public NPCData npcData;
    private float affectionLevel;

    public float GetAffectionLevel()
    {
        return affectionLevel;
    }

    public void SetAffectionLevel(float newLevel)
    {
        affectionLevel = newLevel;
    }

    public bool GiveGift(Item item)
    {
        return false;
    }
}
