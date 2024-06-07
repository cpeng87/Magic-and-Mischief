using System;

public static class MailEventHandler
{
    public static event Action OnMailChanged;

    public static void TriggerMailChangedEvent()
    {
        OnMailChanged?.Invoke();
    }

}
