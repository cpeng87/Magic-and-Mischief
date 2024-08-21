using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public bool isNpc;
    public GameObject panel;
    public GameObject indicator;

    public GameObject namePanel;
    public TextMeshProUGUI nameText;

    public TextMeshProUGUI textbox;

    public GameObject portraitPanel;
    public Image portrait;

    public PlayableDirector playableDirector;

    private bool isTyping;

    private string currText;
    private Coroutine typingCoroutine;

    // private int currentTextLine;

    private void Start()
    {
        panel.SetActive(false);
        playableDirector = FindObjectOfType<PlayableDirector>();
    }

    public void OpenDialogue()
    {
        if (!panel.activeSelf)
        {
            panel.SetActive(true);
            // UnityEngine.Time.timeScale = 0f;
            GameManager.instance.uiManager.StartDialogue();
            if (playableDirector != null)
            {
                playableDirector.Pause();
            }
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
            if (isTyping)
            {
                FinishTyping();
            }
            else
            {
                GameManager.instance.dialogueManager.PushText();
            }
        }
    }

    public void HideNameAndPortrait()
    {
        namePanel.SetActive(false);
        portraitPanel.SetActive(false);
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
        currText = newText;
        typingCoroutine = StartCoroutine(TypeText(newText));
    }

    public IEnumerator TypeText(string text)
    {
        isTyping = true;
        textbox.text = "";
        var waitTimer = new WaitForSecondsRealtime(0.01f);
        foreach (char c in text)
        {
            textbox.text += c;
            yield return waitTimer;
        }
        isTyping = false;
    }

    private void FinishTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        textbox.text = currText;
        isTyping = false;
    }

    public void SetIndicator(bool isActive)
    {
        indicator.SetActive(isActive);
    }

    public void EndDialogue()
    {
        panel.SetActive(false);
        GameManager.instance.uiManager.EndDialogue();
        // if (GameManager.instance.uiManager.activeMenuCount == 0)
        // {
        //     UnityEngine.Time.timeScale = 1f;
        // }
        if (playableDirector != null)
        {
            playableDirector.Resume();
        }
    }
}