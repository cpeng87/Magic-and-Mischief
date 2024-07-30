using System;
using UnityEngine;

public class Buff
{
    public enum BuffType { Speed, Unique }
    public BuffType Type { get; private set; }
    public float Value { get; private set; }
    public float Duration { get; private set; }
    public Action OnBuffBegin { get; private set; }
    public Action OnBuffEnd { get; private set; }
    public Sprite buffImage;
    public float RemainingTime;

    public Buff(BuffType type, float value, float duration, Sprite newBuffImage, Action onBuffBegin = null, Action onBuffEnd = null)
    {
        Type = type;
        Value = value;
        Duration = duration;
        buffImage = newBuffImage;
        RemainingTime = duration;
        OnBuffBegin = onBuffBegin;
        OnBuffEnd = onBuffEnd;
    }
}
