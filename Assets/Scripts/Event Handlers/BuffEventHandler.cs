using System;

public static class BuffEventHandler
{
    public static event Action OnBuffChanged;
    public static event Action OnFlightChanged;

    public static void TriggerBuffChangedEvent()
    {
        OnBuffChanged?.Invoke();
    }
    public static void TriggerFlightChangedEvent()
    {
        OnFlightChanged?.Invoke();
    }
}