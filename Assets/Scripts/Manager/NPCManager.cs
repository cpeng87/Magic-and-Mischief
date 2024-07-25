using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour
{
    public Dictionary<NPCData, string> npcCurrMap = new Dictionary<NPCData, string>();
    public Dictionary<NPCData, GameObject> npcGameObjs = new Dictionary<NPCData, GameObject>();
    public List<GameObject> npcs = new List<GameObject>();
    public Dictionary<int, List<(NPCData, string)>> npcCurrMapEntryDict = new Dictionary<int, List<(NPCData, string)>>();
    public Dictionary<string, bool> npcTalkedDict = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        SetupDictionary();
        LoadInNPCs();
        LoadNPCDialogue();
        UpdateLocations();
        TimeEventHandler.OnHourChanged += UpdateLocations;
        TimeEventHandler.OnMinuteChanged += CheckEntryTimes;
        TimeEventHandler.OnDayChanged += ResetTalked;
    }

    public void UpdateLocation(NPCData npc, string newLocation)
    {
        if (npcCurrMap.ContainsKey(npc))
        {
            npcCurrMap[npc] = newLocation;

            if (SceneManager.GetActiveScene().name == newLocation)
            {
                GameObject newNPC = Instantiate(npcGameObjs[npc]);
                newNPC.layer = LayerMask.NameToLayer("Interactables");
                Renderer npcRenderer = newNPC.GetComponent<Renderer>();
                if (npcRenderer != null)
                {
                    npcRenderer.sortingLayerName = "NPC"; // Set the sorting layer name to "NPC"
                }
            }
        }
    }

    private void CheckEntryTimes()
    {
        if (npcCurrMapEntryDict.ContainsKey(GameManager.instance.timeManager.gameTime.minute))
        {
            foreach ((NPCData, string) npcMap in npcCurrMapEntryDict[GameManager.instance.timeManager.gameTime.minute])
            {
                if (npcMap.Item2 == SceneManager.GetActiveScene().name)
                {
                    GameObject newNPC = Instantiate(npcGameObjs[npcMap.Item1]);
                    newNPC.layer = LayerMask.NameToLayer("Interactables");
                    Renderer npcRenderer = newNPC.GetComponent<Renderer>();
                    if (npcRenderer != null)
                    {
                        npcRenderer.sortingLayerName = "NPC"; // Set the sorting layer name to "NPC"
                    }
                    npcCurrMap[npcMap.Item1] = SceneManager.GetActiveScene().name;
                }
            }
        }
    }

    private void UpdateLocations()
    {
        List<NPCData> npcKeys = new List<NPCData>(npcCurrMap.Keys);

        for (int i = 0; i < npcKeys.Count; i++)
        {
            NPCData npc = npcKeys[i];
            npcCurrMap[npc] = npc.weeklySchedule[GameManager.instance.timeManager.date.day].dailySchedule[GameManager.instance.timeManager.gameTime.hour].map;

            if (npc.weeklySchedule[GameManager.instance.timeManager.date.day].dailySchedule[GameManager.instance.timeManager.gameTime.hour] is PathData)
            {
                PathData pathData = (PathData) npc.weeklySchedule[GameManager.instance.timeManager.date.day].dailySchedule[GameManager.instance.timeManager.gameTime.hour];
                if (pathData.isSwapScene)
                {
                    int entryTime = CalculateTimeEntry(pathData.pathpoints, npc.movementSpeed);

                    if (!npcCurrMapEntryDict.ContainsKey(entryTime))
                    {
                        npcCurrMapEntryDict.Add(entryTime, new List<(NPCData, string)>());
                    }

                    npcCurrMapEntryDict[entryTime].Add((npc, pathData.newMap));
                }
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
                newNPC.layer = LayerMask.NameToLayer("Interactables");
                Renderer npcRenderer = newNPC.GetComponent<Renderer>();
                if (npcRenderer != null)
                {
                    npcRenderer.sortingLayerName = "NPC"; // Set the sorting layer name to "NPC"
                }
            }
        }
    }

    private void LoadNPCDialogue()
    {
        foreach (NPCData npc in npcCurrMap.Keys)
        {
            npc.dialogueStorage.introduction = GameManager.instance.dialogueManager.ParseSpecificDialogue("Introduction", npc.dialogue);
            if (GameManager.instance.dialogueManager.ParseSpecificDialogue("Introduction", npc.dialogue) == null)
            {
                Debug.Log("Intro is null");
            }
            npc.dialogueStorage.possibleDialogues = GameManager.instance.dialogueManager.ParseDialogue(npc.dialogue);
            if (GameManager.instance.dialogueManager.ParseDialogue(npc.dialogue) == null)
            {
                Debug.Log("dialogue is null");
            }
        }
    }

    private void OnDestroy()
    {
        TimeEventHandler.OnHourChanged -= UpdateLocations;
        TimeEventHandler.OnMinuteChanged -= CheckEntryTimes;
        TimeEventHandler.OnDayChanged -= ResetTalked;
    }

    private void SetupDictionary()
    {
        foreach (GameObject npc in npcs)
        {
            NPCData npcData = npc.GetComponent<NPCCharacter>().npcData;
            int day = GameManager.instance.timeManager.date.day;
            npcCurrMap.Add(npcData, npcData.weeklySchedule[day].initialCheckpoint.mapName);
            npcGameObjs.Add(npcData, npc);
            npcTalkedDict.Add(npcData.name, false);
            PlayerPrefs.SetInt(npcData.name, 0);
        }
    }

    private int CalculateTimeEntry(List<Checkpoint> pathpoints, float movementSpeed)
    {
        float totalTime = 0;
        for (int i = 0; i < pathpoints.Count; i++)
        {
            if (pathpoints[i + 1].isSwapScene)
            {
                int rtn = (int) Math.Ceiling(GameManager.instance.timeManager.ConvertRealTimeToMinutes(totalTime));
                return rtn;
            }

            Vector3 startPoint = pathpoints[i].position;
            Vector3 endPoint = pathpoints[i + 1].position;

            float distance = Vector3.Distance(startPoint, endPoint);
            float timeToTravel = distance / movementSpeed;

            totalTime += timeToTravel;
            //needs to convert real world seconds to in game minutes
        }

        // did not find the swap scene
        Debug.Log("Did not find the swap scene.");
        return 0;
    }

    public bool CheckHasTalked(string name)
    {
        if (npcTalkedDict.ContainsKey(name))
        {
            return npcTalkedDict[name];
        }
        Debug.Log("Can't find " + name + " in dict");
        return false;
    }

    private void ResetTalked()
    {
        foreach (string name in npcTalkedDict.Keys)
        {
            npcTalkedDict[name] = false;
        }
    }

    public void SetTalked(string name)
    {
        if (npcTalkedDict.ContainsKey(name))
        {
            npcTalkedDict[name] = true;
            return;
        }
        Debug.Log("Can't find " + name + " in dict");
    }

}
