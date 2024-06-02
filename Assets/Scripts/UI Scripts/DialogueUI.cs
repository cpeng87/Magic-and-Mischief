using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueUI : InteractUI
{
    public GameObject panel;
    public GameObject indicator;

    public override abstract void ToggleUI();
}
