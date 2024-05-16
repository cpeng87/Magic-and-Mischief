using System;

public static class CurrencyEventHandler
{
    public static event Action OnCurrencyChanged;

    public static void TriggerCurrencyChangedEvent()
    {
        OnCurrencyChanged?.Invoke();
    }

}
