using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerUI : InteractTriggerUI<ShopUI>
{
    public ShopData shopData;

    public override void Interact()
    {
        ui.SetShopData(shopData);
        ui.ToggleUI();
    }
}
