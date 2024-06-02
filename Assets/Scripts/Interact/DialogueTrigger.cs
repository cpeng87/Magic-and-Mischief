using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, Interactable
{
    private bool isFile;
    public TextAsset file;
    private string text;
    private DialogueUI ui;
    
    void Start()
    {
        if (isFile)
        {
            ui = FindObjectOfType<NPCDialogueUI>();
        }
        else
        {
            ui = FindObjectOfType<TextDialogueUI>();
        }
    }
    public void Interact()
    {
        ui.ToggleUI();
    }
}
