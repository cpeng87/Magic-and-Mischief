using System;

public static class InventoryEventHandler
{
    public static event Action OnInventoryChanged;
    public static event Action OnSelectedSlotChanged;

    public static void TriggerInventoryChangedEvent()
    {
        OnInventoryChanged?.Invoke();
    }
    public static void TriggerSelectedSlotChangedEvent()
    {
        OnSelectedSlotChanged?.Invoke();
    }
}
