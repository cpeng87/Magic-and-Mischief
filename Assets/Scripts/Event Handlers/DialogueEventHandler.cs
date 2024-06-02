using System;

public static class DialogueEventHandler
{
    public static event Action OnDialogueChanged;

    public static void TriggerDialogueChangedEvent()
    {
        OnDialogueChanged?.Invoke();
    }
}
