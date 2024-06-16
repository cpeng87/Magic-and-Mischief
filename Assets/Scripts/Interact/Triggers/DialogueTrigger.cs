using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : InteractTriggerUI<DialogueUI>
{
    public TextAsset dialogue;

    protected override void Start()
    {
        ui = FindObjectOfType<DialogueUI>();
    }
    public override void Interact()
    {
        if (GameManager.instance.dialogueManager.isActive)
        {
            return;
        }
        ui.ToggleUI();
        GameManager.instance.dialogueManager.Initialize(dialogue);
        // ui.ToggleUI();
    }
}
