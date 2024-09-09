using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NotebookUI : Toggleable
{
    // public GameObject notebookBackground;

    public GameObject notebookPanel;
    public GameObject notebookListingPrefab;
    public GameObject notebookDisplay;

    public GameObject notebookOpenedPanel;

    public GameObject plainTextBox;
    public GameObject imageBasedBox;
    public TextMeshProUGUI imageText;
    public Image image;

    //
    public TextMeshProUGUI plainText;

    private void Start()
    {
        notebookDisplay.SetActive(true);
        notebookOpenedPanel.SetActive(false);
        toggledDisplay.SetActive(false);
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
        }
        else
        {   ReturnToNotebook();
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
        notebookDisplay.SetActive(true);
        imageBasedBox.SetActive(false);
        plainTextBox.SetActive(false);
        notebookPanel.SetActive(true);
        ClearEntries();

        List<(NotebookData, bool)> notebookEntries = GameManager.instance.notebookManager.GetActiveEntries();

        int counter = 0;
        foreach ((NotebookData, bool) entryInfo in notebookEntries)
        {
            GameObject newEntry = Instantiate(notebookListingPrefab, notebookPanel.transform);

            //completed up to here
            NotebookEntryUI notebookEntry = newEntry.GetComponent<NotebookEntryUI>();

            notebookEntry.SetEntryID(counter);

            if (notebookEntry != null)
            {
                notebookEntry.SetTitle(entryInfo.Item1.title.ToString());
                if (entryInfo.Item2)
                {
                    notebookEntry.SetComplete();
                }

                Button button = newEntry.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(() => OpenEntry(notebookEntry));
                }
            }
            else
            {
                Debug.LogWarning("MailboxListing component not found on the instantiated prefab.");
            }

            counter += 1;
        }
    }

    private void ClearEntries()
    {
        foreach (Transform entry in notebookPanel.transform)
        {
            Destroy(entry.gameObject);
        }
    }

    public void OpenEntry(NotebookEntryUI clickedEntry)
    {

        notebookOpenedPanel.SetActive(true);
        notebookDisplay.SetActive(false);

        List<(NotebookData, bool)> activeEntries = GameManager.instance.notebookManager.GetActiveEntries();
        NotebookData notebookData = activeEntries[clickedEntry.GetEntryID()].Item1;
        SetupEntry(notebookData.title);
    }

    public void SetupEntry(string title)
    {
        if (GameManager.instance.notebookManager.GetNotebookStage(title).entryType == EntryType.Image)
        {
            imageBasedBox.SetActive(true);
            plainTextBox.SetActive(false);
            imageText.text = GameManager.instance.notebookManager.GetNotebookStage(title).text;
            image.sprite = GameManager.instance.notebookManager.GetNotebookStage(title).image;
        }
        else if (GameManager.instance.notebookManager.GetNotebookStage(title).entryType == EntryType.PlainText)
        {
            imageBasedBox.SetActive(false);
            plainTextBox.SetActive(true);

            plainText.text = GameManager.instance.notebookManager.GetNotebookStage(title).text;
        }
    }

    public void ReturnToNotebook()
    {
        notebookOpenedPanel.SetActive(false);
        notebookDisplay.SetActive(true);
        Setup();
    }
}
