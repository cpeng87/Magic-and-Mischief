using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffManager : MonoBehaviour
{
    public List<Buff> activeBuffs = new List<Buff>();
    private Dictionary<Buff, Coroutine> buffCoroutines = new Dictionary<Buff, Coroutine>();
    public PlayerMovement playerMovement;
    private int maximumBuffs = 12;
    public bool isFlying;

    public void Start()
    {
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.Log("Player movement is null");
        }
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

    public bool StartSpeedBuff(float speedIncrease, float duration, Sprite icon, Action onBuffBegin, Action onBuffEnd)
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
        Buff speedBuff = new Buff(Buff.BuffType.Speed, speedIncrease, duration, icon, onBuffBegin, onBuffEnd);
        AddBuff(speedBuff);
        return true;
    }
    public bool StartUniqueBuff(float duration, Sprite icon, Action onBuffBegin, Action onBuffEnd)
    {
        if (activeBuffs.Count >= maximumBuffs)
        {
            return false;
        }
        Debug.Log(duration);
        Buff uniqueBuff = new Buff(Buff.BuffType.Unique, 0, duration, icon, onBuffBegin, onBuffEnd);
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

    public void ApplyBuff(Buff buff)
    {
        buff.OnBuffBegin?.Invoke();
        if (buff.Type == Buff.BuffType.Speed)
        {
            if (playerMovement == null)
            {
                playerMovement = this.gameObject.GetComponent<PlayerMovement>();
            }
            playerMovement.IncreaseSpeed(buff.Value);
        }
    }

    public void SetBuffs(List<Buff> buffs)
    {
        foreach (Buff buff in buffs)
        {
            AddBuff(buff);
        }
    }

    private IEnumerator RemoveBuffAfterDuration(Buff buff)
    {
        while (buff.RemainingTime > 0f)
        {
            buff.RemainingTime -= UnityEngine.Time.deltaTime;
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

    public void SetIsFlying(bool newVal)
    {
        Debug.Log("Setting isFlying");
        isFlying = newVal;
    }
}
