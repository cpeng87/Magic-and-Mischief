using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTriggerUI : MonoBehaviour, Interactable
{
    public InteractUI ui;
    public void Interact()
    {
        ui.position = new Vector3Int((int) this.gameObject.transform.position.x, (int) this.gameObject.transform.position.y, (int) this.gameObject.transform.position.z);
        ui.ToggleUI();
    }
}
