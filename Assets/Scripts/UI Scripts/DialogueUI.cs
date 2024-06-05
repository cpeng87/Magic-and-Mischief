using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : InteractUI
{
    public bool isNpc;
    public GameObject panel;
    public GameObject indicator;

    public GameObject namePanel;
    public TextMeshProUGUI nameText;

    public TextMeshProUGUI textbox;

    public GameObject portraitPanel;
    public Image portrait;

    // private int currentTextLine;

    private void Start()
    {
        panel.SetActive(false);
    }

    public override void ToggleUI()
    {
        if (!panel.activeSelf)
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
            GameManager.instance.PushActiveMenu(this.gameObject);
        }
    }

    private void Update()
    {
        if (!panel.activeSelf)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            // PushText();
            // DialogueEventHandler.TriggerDialogueChangedEvent();
            GameManager.instance.dialogueManager.PushText();
        }
    }

    public void UpdateDisplay(string newName, Sprite newPortrait)
    {
        if (newPortrait != null)
        {
            if (!portraitPanel.activeSelf)
            {
                portraitPanel.SetActive(true);
            }
            portrait.sprite = newPortrait;
        }
        if (newName != null)
        {
            if (!namePanel.activeSelf)
            {
                namePanel.SetActive(true);
            }
            nameText.text = newName;
        }
    }

    public void UpdateDisplay(string newText)
    {
        StartCoroutine(TypeText(newText));
    }

    public IEnumerator TypeText(string text)
    {
        textbox.text = "";
        var waitTimer = new WaitForSecondsRealtime(0.01f);
        foreach (char c in text)
        {
            textbox.text = textbox.text + c;
            yield return waitTimer;
        }
    }

    public void SetIndicator(bool isActive)
    {
        indicator.SetActive(isActive);
    }

    public void EndDialogue()
    {
        panel.SetActive(false);
        GameManager.instance.PopActiveMenu();
        if (GameManager.instance.activeMenuCount == 0)
        {
            Time.timeScale = 1f;
        }
    }
}