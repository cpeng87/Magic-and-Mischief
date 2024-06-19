using System;

public static class MailEventHandler
{
    public static event Action OnMailChanged;
    public static event Action OnNewMail;

    public static void TriggerMailChangedEvent()
    {
        OnMailChanged?.Invoke();
    }
    public static void TriggerNewMailEvent()
    {
        OnNewMail?.Invoke();
        OnMailChanged?.Invoke();
    }


}
