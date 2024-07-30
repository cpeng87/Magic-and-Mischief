using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoboardTriggerUI :  InteractTriggerUI<PhotoUI>
{
    public override void Interact()
    {
        ui.ToggleUI();
    }
}
