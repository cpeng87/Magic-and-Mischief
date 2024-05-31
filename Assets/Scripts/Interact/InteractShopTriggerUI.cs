using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractShopTriggerUI : MonoBehaviour, Interactable
{
    public ShopData shopData;
    public ShopUI ui;

    void Start()
    {
        ui = FindObjectOfType<ShopUI>();
    }
    public void Interact()
    {
        // ui.pos = new Vector3Int((int) this.gameObject.transform.position.x, (int) this.gameObject.transform.position.y, (int) this.gameObject.transform.position.z);
        ui.ToggleUI();
    }
}
