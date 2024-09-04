using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotoUI : Toggleable
{
    public Image photo;
    public TextMeshProUGUI text;
    public List<PhotoData> photos;
    // public GameObject photoPanel;
    private int index;

    private void Start()
    {
        toggledDisplay.SetActive(false);
        index = 0;
    }

    public override void ToggleUI()
    {
        if (GameManager.instance.uiManager.isDialogue)
        {
            return;
        }
        if (!toggledDisplay.gameObject.activeSelf)
        {
            toggledDisplay.gameObject.SetActive(true);
            UnityEngine.Time.timeScale = 0f;
            GameManager.instance.uiManager.PushActiveMenu(this);
            Setup();
            CheckStartedNotebook();
        }
        else
        {   
            toggledDisplay.gameObject.SetActive(false);
            GameManager.instance.uiManager.PopActiveMenu();
            if (GameManager.instance.uiManager.GetActiveMenuCount() == 0)
            {
                UnityEngine.Time.timeScale = 1f;
            }
        }
    }

    public override void Setup()
    {
        if (photos.Count > index)
        {
            photo.sprite = photos[index].photo;
            text.text = photos[index].description;
        }
        else
        {
            Debug.Log("Index out of bounds.");
        }
    }

    public void NextPage()
    {
        if (photos.Count > index)
        {
            index += 1;
            Setup();
            CheckStartedNotebook();
        }
    }
    public void PreviousPage()
    {
        if (index > 0)
        {
            index -= 1;
            Setup();
        }
    }

    public void CheckStartedNotebook()
    {
        foreach ((NotebookData, bool) entry in GameManager.instance.notebookManager.GetActiveEntries())
        {
            if (GameManager.instance.notebookManager.CheckIfActive(photos[index].notebookEntryName))
            {
                return;
            }
        }
        GameManager.instance.notebookManager.ActivateQuest(photos[index].notebookEntryName);
    }
}
