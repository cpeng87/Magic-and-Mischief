using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NotebookEntryUI : MonoBehaviour
{
    private int entryID;

    public TextMeshProUGUI title;

    public Image display;
    public Image completedStamp;

    public void SetTitle(string newTitle)
    {
        title.text = newTitle;
    }
    public void SetComplete()
    {
        display.color = new Color(0,0,0,150);
        completedStamp.enabled = true;
    }
    public void SetEntryID(int newEntryID)
    {
        entryID = newEntryID;
    }
    public int GetEntryID()
    {
        return entryID;
    }
}
