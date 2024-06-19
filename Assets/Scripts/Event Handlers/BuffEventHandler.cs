using System;

public static class BuffEventHandler
{
    public static event Action OnBuffChanged;

    public static void TriggerBuffChangedEvent()
    {
        OnBuffChanged?.Invoke();
    }
}