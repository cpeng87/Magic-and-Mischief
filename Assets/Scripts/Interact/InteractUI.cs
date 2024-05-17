using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractUI : MonoBehaviour
{
    public Vector3Int position;
    public abstract void ToggleUI();
}

