using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour
{
    // public List<NPCData> npcs = new List<NPCData>();
    public Dictionary<NPCData, string> npcCurrMap = new Dictionary<NPCData, string>();
    public Dictionary<NPCData, GameObject> npcGameObjs = new Dictionary<NPCData, GameObject>();
    public List<GameObject> npcs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SetupDictionary();
        // TimeEventHandler.OnHourChanged += UpdateLocations;
    }

    public void UpdateLocation(NPCData npc, string newLocation)
    {
        // also needs to check if new instantiation needed if player is on same scene
        if (npcCurrMap.ContainsKey(npc))
        {
            npcCurrMap[npc] = newLocation;

            if (SceneManager.GetActiveScene().name == newLocation)
            {
                GameObject newNPC = Instantiate(npcGameObjs[npc]);
                newNPC.layer = LayerMask.NameToLayer("NPC");
            }
        }
    }

    public void LoadInNPCs()
    {
        foreach (NPCData npc in npcCurrMap.Keys)
        {
            if (npcCurrMap[npc] == SceneManager.GetActiveScene().name)
            {
                GameObject newNPC = Instantiate(npcGameObjs[npc]);
            }
        }
    }

    // private void OnDestroy()
    // {
    //     TimeEventHandler.OnHourChanged -= UpdateLocations;
    // }

    private void SetupDictionary()
    {
        foreach (GameObject npc in npcs)
        {
            NPCData npcData = npc.GetComponent<NPCCharacter>().npcData;
            int day = GameManager.instance.timeManager.date.day;
            npcCurrMap.Add(npcData, npcData.weeklySchedule[day].initialCheckpoint.mapName);
            npcGameObjs.Add(npcData, npc);
            PlayerPrefs.SetInt(npcData.name, 0);
        }
    }

}
