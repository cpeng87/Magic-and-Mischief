using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractUI : MonoBehaviour
{
    public Vector3Int pos;
    public abstract void ToggleUI();
}

