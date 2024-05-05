using System;

public static class InventoryEventHandler
{
    public static event Action OnInventoryChanged;

    public static void TriggerInventoryChangedEvent()
    {
        OnInventoryChanged?.Invoke();
    }
}
