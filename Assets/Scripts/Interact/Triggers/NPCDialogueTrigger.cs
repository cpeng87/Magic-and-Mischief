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
        if (GameManager.instance.dialogueManager.isActive)
        {
            return;
        }
        ui.ToggleUI();
        GameManager.instance.dialogueManager.Initialize(npcData.name, npcData.portrait, npcData.dialogue);
    }
}
