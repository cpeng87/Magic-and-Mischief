using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    private int currentTextLine;
    private DialogueUI dialogueUI;
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

    public void Initialize(string newName, Sprite portrait, TextAsset dialogue)
    {
        SelectDialogue(dialogue);
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

    private void Conclude()
    {
        isActive = false;
        dialogueUI.EndDialogue();
    }

    private void SelectDialogue(TextAsset dialogue)
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
        int randomIndex = Random.Range(0, possibleDialogue.Count);
        selectedDialogue = possibleDialogue[randomIndex];

    }
}
