using System;

public static class TimeEventHandler
{
    public static event Action OnMinuteChanged;
    public static event Action OnHourChanged;
    public static event Action OnDayChanged;
    public static event Action OnSeasonChanged;
    public static event Action OnYearChanged;

    public static void TriggerMinuteChangedEvent()
    {
        OnMinuteChanged?.Invoke();
    }
    public static void TriggerHourChangedEvent()
    {
        OnHourChanged?.Invoke();
    }
    public static void TriggerDayChangedEvent()
    {
        OnDayChanged?.Invoke();
    }
    public static void TriggerSeasonChangedEvent()
    {
        OnSeasonChanged?.Invoke();
    }
    public static void TriggerYearChangedEvent()
    {
        OnYearChanged?.Invoke();
    }
}
