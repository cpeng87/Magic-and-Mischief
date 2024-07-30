using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    private List<string> events;
    void Start()
    {
        // npcs = GameManager.instance.npcManager.npcCurrMap;
        // foreach (NPCData npc in GameManager.instance.npcManager.npcCurrMap.Keys)
        // {
        //     PlayerPrefs.SetInt("Introduction" + npc.name, 0);
        // }
        Dictionary<NPCData, NPCInfo> dict = GameManager.instance.npcManager.GetInfoDict();
        foreach (NPCData npc in dict.Keys)
        {
            PlayerPrefs.SetInt("Introduction" + npc.name, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
