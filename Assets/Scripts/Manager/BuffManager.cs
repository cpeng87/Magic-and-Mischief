using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffManager : MonoBehaviour
{
    private List<Buff> activeBuffs = new List<Buff>();
    private Dictionary<Buff, Coroutine> buffCoroutines = new Dictionary<Buff, Coroutine>();
    private PlayerMovement playerMovement;
    private int maximumBuffs = 12;

    private void Start()
    {
        playerMovement = GameManager.instance.player.GetComponent<PlayerMovement>();
    }

    public bool StartFlightBuff(float speedIncrease, float duration, Action onBuffEnd)
    {
        if (activeBuffs.Count >= maximumBuffs)
        {
            return false;
        }
        Buff speedBuff = new Buff(Buff.BuffType.Speed, speedIncrease, duration, onBuffEnd);
        AddBuff(speedBuff);
        return true;
    }

    private void AddBuff(Buff buff)
    {
        activeBuffs.Add(buff);
        ApplyBuff(buff);

        Coroutine buffCoroutine = StartCoroutine(RemoveBuffAfterDuration(buff));
        buffCoroutines[buff] = buffCoroutine;
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
        yield return new WaitForSeconds(buff.Duration);
        RemoveBuff(buff);
    }

    // private IEnumerator RemoveBuffAndStopFlightAfterDuration(Buff buff)
    // {
    //     yield return new WaitForSeconds(buff.Duration);
    //     RemoveBuff(buff);
    //     GameManager.instance.player.pa.StopAnimateFlight();
    // }

    private void RemoveBuff(Buff buff)
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
    }
}
