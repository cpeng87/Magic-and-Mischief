using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : InteractTriggerUI<DialogueUI>
{
    public NPCData npcData;
    private bool hasTalked = false;

    protected override void Start()
    {
        ui = FindObjectOfType<DialogueUI>();
        TimeEventHandler.OnDayChanged += RefreshHasTalked;
    }

    private void RefreshHasTalked()
    {
        hasTalked = false;
    }

    public override void Interact()
    {
        if (GameManager.instance.dialogueManager.isActive || hasTalked)
        {
            return;
        }
        ui.ToggleUI();
        GameManager.instance.dialogueManager.Initialize(npcData.name, npcData.portrait, npcData.dialogue);
        // ui.ToggleUI();
        hasTalked = true;
    }
    private void OnDestroy()
    {
        TimeEventHandler.OnDayChanged -= RefreshHasTalked;
    }
}
