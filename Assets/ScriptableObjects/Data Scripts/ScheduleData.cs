using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/ScheduleData")]
public class ScheduleData : ScriptableObject
{
    public int day;
    public Checkpoint initialCheckpoint;
    public List<ActivityData> dailySchedule;
}
