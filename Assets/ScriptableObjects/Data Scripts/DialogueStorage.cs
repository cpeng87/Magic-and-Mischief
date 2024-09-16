using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/DialogueStorage")]
public class DialogueStorage : ScriptableObject
{
    public Dictionary<string, List<List<string>>> possibleDialogues;
}

