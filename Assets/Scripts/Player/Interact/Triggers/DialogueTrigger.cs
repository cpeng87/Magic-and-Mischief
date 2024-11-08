using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : InteractTriggerUI<DialogueUI>
{
    [TextAreaAttribute]
    public string dialogue;

    protected override void Start()
    {
        ui = FindObjectOfType<DialogueUI>();
    }
    public override void Interact()
    {
        if (GameManager.instance.dialogueManager.GetIsActive())
        {
            return;
        }
        ui.OpenDialogue();
        GameManager.instance.dialogueManager.Initialize(dialogue);
    }
}
