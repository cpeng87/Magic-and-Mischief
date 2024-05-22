using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTriggerUI : MonoBehaviour, Interactable
{
    public InteractUI ui;
    public void Interact()
    {
        // Debug.Log("made it to interact trigger ui");
        ui.pos = new Vector3Int((int) this.gameObject.transform.position.x, (int) this.gameObject.transform.position.y, (int) this.gameObject.transform.position.z);
        ui.ToggleUI();
    }
}
