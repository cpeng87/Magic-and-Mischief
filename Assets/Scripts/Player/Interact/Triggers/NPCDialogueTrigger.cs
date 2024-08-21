using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : InteractTriggerUI<DialogueUI>
{
    private NPCData npcData;

    protected override void Start()
    {
        npcData = GetComponent<NPCCharacter>().npcData;
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
        GameManager.instance.dialogueManager.Initialize(npcData.name, npcData.portrait, npcData.dialogueStorage);
        GameManager.instance.npcManager.SetTalked(npcData);
    }
}
