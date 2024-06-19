using System;
public class Buff
{
    public enum BuffType { Speed }
    public BuffType Type { get; private set; }
    public float Value { get; private set; }
    public float Duration { get; private set; }
    public Action OnBuffEnd { get; private set; }

    public Buff(BuffType type, float value, float duration, Action onBuffEnd = null)
    {
        Type = type;
        Value = value;
        Duration = duration;
        OnBuffEnd = onBuffEnd;
    }
}
