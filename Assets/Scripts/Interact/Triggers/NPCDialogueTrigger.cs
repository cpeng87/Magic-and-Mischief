using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : InteractTriggerUI<DialogueUI>
{
    public NPCData npcData;
    // public bool isFile;
    // public TextAsset file;
    // public string text;
    // private DialogueUI ui;
    
    // protected override void Start()
    // {
    //     if (isFile)
    //     {
    //         ui = FindObjectOfType<NPCDialogueUI>();
    //     }
    //     else
    //     {
    //         ui = FindObjectOfType<TextDialogueUI>();
    //     }
    // }
    public override void Interact()
    {
        GameManager.instance.dialogueManager.Initialize(npcData.name, npcData.portrait, npcData.dialogue);
        ui.ToggleUI();
    }
}
