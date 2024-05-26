using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRepairableTriggerUI : MonoBehaviour, Interactable
{
    public RepairUI ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<RepairUI>();
    }

    public void Interact()
    {
        ui.SetToBeChanged(this.gameObject);
        ui.ToggleUI();
    }
}

