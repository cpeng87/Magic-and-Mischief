using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]
public class Material : Item
{
    // public MaterialData materialData;
    [HideInInspector] public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override bool Use()
    {
        GameObject checkRaycast = GameManager.instance.player.pm.CheckRaycast();
        if (checkRaycast == null)
        {
            return false;
        }
        NPCCharacter npcCharacter = checkRaycast.GetComponent<NPCCharacter>();
        if (npcCharacter != null)
        {   InventoryManager inventory = GameManager.instance.player.inventory;
            if (inventory.GetInventory("Toolbar").selectedSlot != null)
            {
                Item item = GameManager.instance.itemManager.GetItemByName(inventory.GetInventory("Toolbar").selectedSlot.itemName);
                bool result = npcCharacter.GiveGift(item);
                Debug.Log("New affection" + npcCharacter.GetAffectionLevel());
                return true;
            }
        }
        return false;
    }
}
