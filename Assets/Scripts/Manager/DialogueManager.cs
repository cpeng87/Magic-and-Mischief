using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    private int currentTextLine;
    public DialogueUI dialogueUI;
    private List<string> selectedDialogue;
    public bool isActive;

    public void Load()
    {
        dialogueUI = FindObjectOfType<DialogueUI>();
        isActive = false;
    }
    public void PushText()
    {
        currentTextLine += 1;
        if (currentTextLine >= selectedDialogue.Count)
        {
            Conclude();
        }
        else
        {
            dialogueUI.UpdateDisplay(selectedDialogue[currentTextLine]);
            if (selectedDialogue.Count - 1 == currentTextLine)
            {
                dialogueUI.SetIndicator(false);
            }
        }
    }

    //for random npc talk
    public void Initialize(string newName, Sprite portrait, DialogueStorage dialogueStorage)
    {
        if (PlayerPrefs.GetInt(newName, 0) == 0)
        {
            // SelectSpecificDialogue("Introduction", dialogue);
            selectedDialogue = dialogueStorage.introduction;
            PlayerPrefs.SetInt(newName, 1);
        }
        else
        {
            // SelectRandomDialogue(dialogue);
            int rand = Random.Range(0, dialogueStorage.possibleDialogues.Count);
            selectedDialogue = dialogueStorage.possibleDialogues[rand];
        }

        currentTextLine = 0;

        dialogueUI.UpdateDisplay(newName, portrait);
        dialogueUI.UpdateDisplay(selectedDialogue[currentTextLine]);
        if (selectedDialogue.Count - 1 == currentTextLine)
        {
            dialogueUI.SetIndicator(false);
        }
        else{
            dialogueUI.SetIndicator(true);
        }
        isActive = true;
    }

    public void Initialize(string dialogue)
    {
        dialogueUI.HideNameAndPortrait();
        selectedDialogue = new List<string>();
        selectedDialogue.Add(dialogue);
        dialogueUI.UpdateDisplay(dialogue);
        dialogueUI.SetIndicator(false);
        isActive = true;
    }

    private void Conclude()
    {
        currentTextLine = 0;
        isActive = false;
        dialogueUI.EndDialogue();
    }

    public List<string> ParseSpecificDialogue(string dialogueTag, TextAsset dialogue)
    {
        using (StringReader reader = new StringReader(dialogue.text))
        {
            string line;
            List<string> currDialogue = null;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Contains("[" + dialogueTag + "]"))
                {
                    currDialogue = new List<string>();
                }
                else if (line.Contains("[EndDialogue]") && currDialogue != null)
                {
                    return currDialogue;
                }
                else if (!string.IsNullOrEmpty(line) && currDialogue != null)
                {
                    currDialogue.Add(line);
                }
            }
        }
        return null;
    }

    public List<List<string>> ParseDialogue(TextAsset dialogue)
    {
        List<List<string>> possibleDialogue = new List<List<string>>();
        using (StringReader reader = new StringReader(dialogue.text))
        {
            List<string> currDialogue = null;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Contains("[BeginDialogue]"))
                {
                    currDialogue = new List<string>();
                }
                else if (line.Contains("[EndDialogue]"))
                {
                    if (currDialogue != null)
                    {
                        possibleDialogue.Add(currDialogue);
                        currDialogue = null;
                    }
                }
                else if (line == "" || (line.Contains("[") && line.Contains("]")))
                {
                    continue;
                }
                else
                {
                    if (currDialogue != null)
                    {
                        currDialogue.Add(line);
                    }
                }
            }
        }
        return possibleDialogue;
    }
}
