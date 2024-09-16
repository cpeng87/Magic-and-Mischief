using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : InteractTriggerUI<DialogueUI>
{
    private NPCData npcData;
    public NPCCharacter npcChar;

    protected override void Start()
    {
        npcChar = GetComponent<NPCCharacter>();
        npcData = npcChar.npcData;
        ui = FindObjectOfType<DialogueUI>();
    }
    public override void Interact()
    {
        if (GameManager.instance.dialogueManager.GetIsActive())
        {
            return;
        }
        if (GameManager.instance.npcManager.CheckHasTalked(npcData))
        {
            return;
        }
        ui.OpenDialogue();
        if (npcChar == null)
        {
            Debug.Log("npc char is null");
        }
        GameManager.instance.dialogueManager.Initialize(npcChar);
        // GameManager.instance.npcManager.SetTalked(npcData);
    }
    public void GiftDialogue(string tag)
    {
        if (GameManager.instance.dialogueManager.GetIsActive())
        {
            return;
        }
        ui.OpenDialogue();
        GameManager.instance.dialogueManager.Initialize(npcChar, tag);
    }
}
