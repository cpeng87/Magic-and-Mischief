using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    private int currentTextLine;
    private DialogueUI dialogueUI;
    private List<string> selectedDialogue;
    private bool isActive;

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

    //for giftgiving
    public void Initialize(NPCCharacter npcChar, string tag)
    {
        NPCData npcData = npcChar.npcData;
        selectedDialogue = npcData.dialogueStorage.possibleDialogues[tag][0];

        currentTextLine = 0;

        dialogueUI.UpdateDisplay(npcData.npcName, npcData.portrait);
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

    public void Initialize(NPCCharacter npcChar)
    {
        if (npcChar == null)
        {
            Debug.Log("npc char is null");
        }
        NPCData npcData = npcChar.npcData;
        if (PlayerPrefs.GetInt(npcData.npcName, 0) == 0)
        {
            selectedDialogue = npcData.dialogueStorage.possibleDialogues["Introduction"][0];
            PlayerPrefs.SetInt(npcData.npcName, 1);
        }
        else
        {
            // SelectRandomDialogue(dialogue); needs to onclude logic on heart levels
            int rand = Random.Range(0, npcData.dialogueStorage.possibleDialogues["NoCondition"].Count);
            selectedDialogue = npcData.dialogueStorage.possibleDialogues["NoCondition"][rand];
        }

        currentTextLine = 0;

        dialogueUI.UpdateDisplay(npcData.npcName, npcData.portrait);
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

    // //for random npc talk
    // public void Initialize(string newName, Sprite portrait, DialogueStorage dialogueStorage)
    // {
    //     if (PlayerPrefs.GetInt(newName, 0) == 0)
    //     {
    //         // SelectSpecificDialogue("Introduction", dialogue);
    //         selectedDialogue = dialogueStorage.introduction;
    //         PlayerPrefs.SetInt(newName, 1);
    //     }
    //     else
    //     {
    //         // SelectRandomDialogue(dialogue); needs to onclude logic on heart levels
    //         int rand = Random.Range(0, dialogueStorage.possibleDialogues.Count);
    //         selectedDialogue = dialogueStorage.possibleDialogues["NoCondition"][rand];
    //     }

    //     currentTextLine = 0;

    //     dialogueUI.UpdateDisplay(newName, portrait);
    //     dialogueUI.UpdateDisplay(selectedDialogue[currentTextLine]);
    //     if (selectedDialogue.Count - 1 == currentTextLine)
    //     {
    //         dialogueUI.SetIndicator(false);
    //     }
    //     else{
    //         dialogueUI.SetIndicator(true);
    //     }
    //     isActive = true;
    // }

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

    public Dictionary<string, List<List<string>>> ParseDialogue(TextAsset dialogue)
    {
        List<List<string>> possibleDialogue = new List<List<string>>();
        Dictionary<string, List<List<string>>> conditionalDialogue = new Dictionary<string, List<List<string>>>();
        using (StringReader reader = new StringReader(dialogue.text))
        {
            List<string> currDialogue = null;
            string tag = null;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Contains("[BeginDialogue"))
                {
                    currDialogue = new List<string>();
                    tag = null;
                    int equalsIndex = line.IndexOf('=');
                    int endBracketIndex = line.IndexOf(']');
                    if (equalsIndex != -1 && endBracketIndex != -1)
                    {
                        tag = line.Substring(equalsIndex + 1, endBracketIndex - equalsIndex - 1);
                        Debug.Log(tag);
                    }
                    else
                    {
                        tag = "NoCondition";
                    }
                }
                else if (line.Contains("[EndDialogue]"))
                {
                    if (currDialogue != null)
                    {
                        if (tag != null)
                        {
                            //adding the new tag to the conditional dialogue dict
                            if (!conditionalDialogue.ContainsKey(tag))
                            {
                                conditionalDialogue.Add(tag, new List<List<string>>());
                            }
                            conditionalDialogue[tag].Add(currDialogue);
                        }
                        else
                        {
                            possibleDialogue.Add(currDialogue);
                        }
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
        return conditionalDialogue;
    }

    public bool GetIsActive()
    {
        return isActive;
    }
}
