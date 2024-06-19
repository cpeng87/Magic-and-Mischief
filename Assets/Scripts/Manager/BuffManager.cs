using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffManager : MonoBehaviour
{
    public List<Buff> activeBuffs = new List<Buff>();
    private Dictionary<Buff, Coroutine> buffCoroutines = new Dictionary<Buff, Coroutine>();
    private PlayerMovement playerMovement;
    private int maximumBuffs = 12;

    private void Start()
    {
        playerMovement = GameManager.instance.player.GetComponent<PlayerMovement>();
    }

    private Buff CheckDuplicateBuff(Sprite toBeChecked)
    {
        foreach (Buff buff in activeBuffs)
        {
            if (buff.buffImage == toBeChecked)
            {
                return buff;
            }
        }
        return null;
    }

    public bool StartSpeedBuff(float speedIncrease, float duration, Sprite icon, Action onBuffEnd)
    {
        if (activeBuffs.Count >= maximumBuffs)
        {
            return false;
        }
        Buff duplicate = CheckDuplicateBuff(icon);
        if (duplicate != null)
        {
            //refresh buff timer
            RemoveBuff(duplicate);
        }
        Buff speedBuff = new Buff(Buff.BuffType.Speed, speedIncrease, duration, icon, onBuffEnd);
        AddBuff(speedBuff);
        return true;
    }
    public bool StartUniqueBuff(float duration, Sprite icon, Action onBuffEnd)
    {
        if (activeBuffs.Count >= maximumBuffs)
        {
            return false;
        }
        Buff uniqueBuff = new Buff(Buff.BuffType.Unique, 0, duration, icon, onBuffEnd);
        AddBuff(uniqueBuff);
        return true;
    }

    private void AddBuff(Buff buff)
    {
        activeBuffs.Add(buff);
        ApplyBuff(buff);

        Coroutine buffCoroutine = StartCoroutine(RemoveBuffAfterDuration(buff));
        buffCoroutines[buff] = buffCoroutine;
        BuffEventHandler.TriggerBuffChangedEvent();
    }

    private void ApplyBuff(Buff buff)
    {
        if (buff.Type == Buff.BuffType.Speed)
        {
            playerMovement.IncreaseSpeed(buff.Value);
        }
    }

    private IEnumerator RemoveBuffAfterDuration(Buff buff)
    {
        while (buff.RemainingTime > 0f)
        {
            buff.RemainingTime -= Time.deltaTime;
            yield return null;
        }
        RemoveBuff(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        if (buff.Type == Buff.BuffType.Speed)
        {
            playerMovement.DecreaseSpeed(buff.Value);
        }

        activeBuffs.Remove(buff);
        if (buffCoroutines.ContainsKey(buff))
        {
            StopCoroutine(buffCoroutines[buff]);
            buffCoroutines.Remove(buff);
        }

        buff.OnBuffEnd?.Invoke();
        BuffEventHandler.TriggerBuffChangedEvent();
    }

    public void RemoveBuff(int buffID)
    {
        RemoveBuff(activeBuffs[buffID]);
    }
}
