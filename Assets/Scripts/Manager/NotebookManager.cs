using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookEntryInfo
{
    public NotebookData notebookData;
    public bool isCompleted;
    public int index;

    public NotebookEntryInfo(NotebookData notebookData)
    {
        this.notebookData = notebookData;
        isCompleted = false;
        index = 0;
    }
}

public class NotebookManager : MonoBehaviour
{
    public List<NotebookData> notebookEntries = new List<NotebookData>();
    public Dictionary<string, NotebookEntryInfo> activeNotebookEntries = new Dictionary<string, NotebookEntryInfo>();

    void Start()
    {
        foreach(NotebookData entry in notebookEntries)
        {
            activeNotebookEntries.Add(entry.title, new NotebookEntryInfo(entry));
        }
    }

    public void IncrementNotebookEntryIndex(string toBeUpdated)
    {
        if (activeNotebookEntries.ContainsKey(toBeUpdated))
        {
            activeNotebookEntries[toBeUpdated].index += 1;
            if (activeNotebookEntries[toBeUpdated].index > activeNotebookEntries[toBeUpdated].notebookData.stages.Count)
            {
                activeNotebookEntries[toBeUpdated].isCompleted = true;
            }
        }
    }

    public List<(NotebookData, bool)> GetActiveEntries()
    {
        List<(NotebookData, bool)> rtn = new List<(NotebookData, bool)>();
        foreach (string entry in activeNotebookEntries.Keys)
        {
            rtn.Add((activeNotebookEntries[entry].notebookData, activeNotebookEntries[entry].isCompleted));
        }
        return rtn;
    }
}
