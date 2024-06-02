using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/DialogueContainer")]
public class DialogueContainer : ScriptableObject
{
    public List<string> line;
    public NPCData npc;
}
