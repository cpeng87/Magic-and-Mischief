using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTriggerUI : MonoBehaviour, Interactable
{
    public InteractUI ui;
    public void Interact()
    {
        ui.ToggleUI();
    }
}
