using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/DialogueStorage")]
public class DialogueStorage : ScriptableObject
{
    public List<string> introduction;
    public List<List<string>> possibleDialogues;
}

