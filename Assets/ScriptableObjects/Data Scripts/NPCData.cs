using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/NPCData")]
public class NPCData : ScriptableObject
{
    public string npcName;
    public Sprite portrait;
    public TextAsset dialogue;
    public float movementSpeed;

    public List<ScheduleData> weeklySchedule;
}
