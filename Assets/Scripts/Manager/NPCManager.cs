using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour
{
    // public List<NPCData> npcs = new List<NPCData>();
    public Dictionary<NPCData, string> npcCurrMap = new Dictionary<NPCData, string>();
    public Dictionary<NPCData, GameObject> npcGameObjs = new Dictionary<NPCData, GameObject>();
    public List<GameObject> npcs = new List<GameObject>();
    public Dictionary<int, List<(NPCData, string)>> npcCurrMapEntryDict = new Dictionary<int, List<(NPCData, string)>>();
    // public Dictionary<int, (List<(NPCData, string)>)> npcCurrMapEntryDict = new Dictionary<int, (List<(NPCData, string)>)>();

    // Start is called before the first frame update
    void Start()
    {
        SetupDictionary();
        LoadInNPCs();
        UpdateLocations();
        TimeEventHandler.OnHourChanged += UpdateLocations;
        TimeEventHandler.OnMinuteChanged += CheckEntryTimes;
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
                // if (pathData.newMap == SceneManager.GetActiveScene().name)
                // {
                //     int entryTime = CalculateTimeEntry();  // what about the others in diff maps -> calc change too?

                //     if (!npcCurrMapEntryDict.ContainsKey(entryTime))
                //     {
                //         npcCurrMapEntryDict.Add(entryTime, new List<(NPCData, string)>());
                //     }

                //     npcCurrMapEntryDict[entryTime].Add((npc, pathData.newMap));
                // }

                // else
                // {
                //     int entryTime = CalculateTimeEntry();
                // }
            }
        }

        // foreach (NPCData npc in npcCurrMap.Keys)
        // {
        //     npcCurrMap[npc] = npc.weeklySchedule[GameManager.instance.timeManager.date.day].dailySchedule[GameManager.instance.timeManager.gameTime.hour].map;
        //     if (npc.weeklySchedule[GameManager.instance.timeManager.date.day].dailySchedule[GameManager.instance.timeManager.gameTime.hour] is PathData)
        //     {
        //         PathData pathData = (PathData) npc.weeklySchedule[GameManager.instance.timeManager.date.day].dailySchedule[GameManager.instance.timeManager.gameTime.hour];
        //         if (pathData.newMap == SceneManager.GetActiveScene().name)
        //         {
        //             int entryTime = CalculateTimeEntry();
        //             if (npcCurrMapEntryDict.ContainsKey(entryTime) == false)
        //             {
        //                 npcCurrMapEntryDict.Add(entryTime, new List<NPCData>());
        //                 npcCurrMapEntryDict[entryTime].Add(npc);
        //             }
        //         }
        //     }
        // }
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

    private void OnDestroy()
    {
        TimeEventHandler.OnHourChanged -= UpdateLocations;
        TimeEventHandler.OnMinuteChanged -= CheckEntryTimes;
    }

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

}
