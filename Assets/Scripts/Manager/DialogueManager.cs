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
    public void Initialize(string newName, Sprite portrait, TextAsset dialogue)
    {
        if (PlayerPrefs.GetInt(newName, 0) == 0)
        {
            SelectSpecificDialogue("Introduction", dialogue);
            PlayerPrefs.SetInt(newName, 1);
        }
        else
        {
            SelectRandomDialogue(dialogue);
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

    public void Initialize(TextAsset dialogue)
    {
        SelectAllDialogue(dialogue);
        dialogueUI.HideNameAndPortrait();
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

    private void SelectAllDialogue(TextAsset dialogue)
    {
        using (StringReader reader = new StringReader(dialogue.text))
        {
            List<string> currDialogue = new List<string>();
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
                    selectedDialogue = currDialogue;
                    return;
                }
                else if (line == "")
                {
                    continue;
                }
                else
                {
                    currDialogue.Add(line);
                }
            }
        }
    }

    private void SelectRandomDialogue(TextAsset dialogue)
    {
        //parse through .txt file to select a line
        List<List<string>> possibleDialogue = new List<List<string>>();
        using (StringReader reader = new StringReader(dialogue.text))
        {
            List<string> currDialogue = new List<string>();
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
                    possibleDialogue.Add(currDialogue);
                    currDialogue = null;
                }
                else if (line == "" || (line.Contains("[") && line.Contains("]")))
                {
                    continue;
                }
                else
                {
                    currDialogue.Add(line);
                }
            }
        }
        int randomIndex = Random.Range(0, possibleDialogue.Count);
        selectedDialogue = possibleDialogue[randomIndex];

    }

    public void SelectSpecificDialogue(string dialogueTag, TextAsset dialogue)
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
                    selectedDialogue = currDialogue;
                    return;
                }
                else if (!string.IsNullOrEmpty(line) && currDialogue != null)
                {
                    currDialogue.Add(line);
                }
            }
        }
    }

}
