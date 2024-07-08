using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Checkpoint
{
    public string mapName;
    public bool isSwapScene;
    public string newMap;
    public Vector3 position;
}