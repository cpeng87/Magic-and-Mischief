using System.Collections.Generic;
using UnityEngine;

public class NotebookEntryInfo
{
    public NotebookData notebookData;
    public bool isCompleted;
    public int currentStage;
    public bool isFavorited;
    public bool isActive;

    public NotebookEntryInfo(NotebookData notebookData)
    {
        this.notebookData = notebookData;
        isCompleted = false;
        isFavorited = false;
        isActive = false;
        currentStage = 0;
    }

    // Increment the current stage of the quest and check if it is completed
    public void AdvanceStage()
    {
        if (!isCompleted)
        {
            currentStage++;
            if (currentStage >= notebookData.stages.Count)
            {
                isCompleted = true;
            }
        }
    }
}

public class NotebookManager : MonoBehaviour
{
    public List<NotebookData> notebookEntries = new List<NotebookData>();
    public Dictionary<string, NotebookEntryInfo> activeNotebookEntries = new Dictionary<string, NotebookEntryInfo>();

    void Start()
    {
        foreach (NotebookData entry in notebookEntries)
        {
            if (!activeNotebookEntries.ContainsKey(entry.title))
            {
                activeNotebookEntries[entry.title] = new NotebookEntryInfo(entry);
            }
        }
    }

    // Activate a quest (if it is not already active) and add it to the active quests list
    public void ActivateQuest(string title)
    {
        if (activeNotebookEntries.ContainsKey(title))
        {
            activeNotebookEntries[title].isActive = true;
        }
        else
        {
            Debug.LogWarning($"Quest with title '{title}' not found in notebook entries.");
        }
    }

    // Increment the current stage of a quest and check if it is completed
    public void AdvanceQuest(string title)
    {
        if (activeNotebookEntries.ContainsKey(title) && activeNotebookEntries[title].isActive)
        {
            activeNotebookEntries[title].AdvanceStage();
        }
        else
        {
            Debug.LogWarning($"Quest with title '{title}' is not active or not found.");
        }
    }

    // Get a list of active quests along with their completion status
    public List<(NotebookData, bool)> GetActiveEntries()
    {
        List<(NotebookData, bool)> activeEntries = new List<(NotebookData, bool)>();
        foreach (var entry in activeNotebookEntries.Values)
        {
            if (entry.isActive)
            {
                activeEntries.Add((entry.notebookData, entry.isCompleted));
            }
        }
        return activeEntries;
    }

    public bool CheckIfActive(string title)
    {
        if (activeNotebookEntries.ContainsKey(title))
        {
            return activeNotebookEntries[title].isActive;
        }
        return false;
    }

    // Get the current stage of a specific quest
    public int GetNotebookIndex(string title)
    {
        if (activeNotebookEntries.ContainsKey(title))
        {
            return activeNotebookEntries[title].currentStage;
        }
        else
        {
            Debug.LogWarning($"Quest with title '{title}' not found.");
            return -1;
        }
    }

    public NotebookStage GetNotebookStage(string title)
    {
        if (activeNotebookEntries.ContainsKey(title))
        {
            return activeNotebookEntries[title].notebookData.stages[GetNotebookIndex(title)];
        }
        else
        {
            Debug.LogWarning($"Quest with title '{title}' not found.");
            return null;
        }
    }

}