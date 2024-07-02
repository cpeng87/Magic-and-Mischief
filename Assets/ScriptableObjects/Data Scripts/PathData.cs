using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PathData", menuName = "NPC/PathData", order = 2)]
public class PathData : ScriptableObject
{
    // public Checkpoint start;
    // public Checkpoint end;
    // public List<Vector3> pathpoints = new List<Vector3>();
    public List<Checkpoint> pathpoints = new List<Checkpoint>();
}
