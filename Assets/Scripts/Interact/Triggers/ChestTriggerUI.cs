using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTriggerUI: InteractTriggerUI<ChestUI>
{
    public override void Interact()
    {
        // ui.pos = new Vector3Int((int) this.gameObject.transform.position.x, (int) this.gameObject.transform.position.y, (int) this.gameObject.transform.position.z);
        ui.SetPosition(new Vector3Int((int) this.gameObject.transform.position.x, (int) this.gameObject.transform.position.y, (int) this.gameObject.transform.position.z));
        ui.ToggleUI();
    }
}
