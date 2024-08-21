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
    public TextMeshProUGUI entryText;

    private void Start()
    {
        notebookDisplay.SetActive(true);
        notebookOpenedPanel.SetActive(false);
        toggledDisplay.SetActive(false);
    }

    private void OpenNotebook()
    {
        ToggleUI();
    }

    // public override void ToggleUI()
    // {
    //     if (!notebookBackground.activeSelf)
    //     {
    //         notebookBackground.SetActive(true);
    //         UnityEngine.Time.timeScale = 0f;
    //         GameManager.instance.uiManager.PushActiveMenu(this);
    //         Setup();
    //     }
    //     else
    //     {   
    //         notebookBackground.SetActive(false);
    //         GameManager.instance.uiManager.PopActiveMenu();
    //         if (GameManager.instance.uiManager.GetActiveMenuCount() == 0)
    //         {
    //             UnityEngine.Time.timeScale = 1f;
    //         }
    //     }
    // }

    public override void Setup()
    {
        notebookDisplay.SetActive(true);
        notebookPanel.SetActive(true);
        ClearEntries();
        Debug.Log("Setting up notebook");

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

    private void OnDestroy()
    {
        // MailEventHandler.OnMailChanged -= SetupMailbox;
    }

    public void OpenEntry(NotebookEntryUI clickedEntry)
    {
        Debug.Log("Opening entry");
        // notebookDisplay.SetActive(false);

        notebookOpenedPanel.SetActive(true);
        notebookDisplay.SetActive(false);

        List<(NotebookData, bool)> activeEntries = GameManager.instance.notebookManager.GetActiveEntries();
        NotebookData notebookData = activeEntries[clickedEntry.GetEntryID()].Item1;
        SetupEntry(notebookData);
    }

    public void SetupEntry(NotebookData data)
    {
        entryText.text = data.stages[data.index].text;
    }

    public void ReturnToNotebook()
    {
        notebookOpenedPanel.SetActive(false);
        notebookDisplay.SetActive(true);
        Setup();
    }
}
