using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Giftable : Item
{
    public override bool Use()
    {
        if (!GameManager.instance.uiManager.CheckNoMenusOpen())
        {
            return false;
        }
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
                //needs to be changed based on bool result
                bool result = npcCharacter.GiveGift(item);
                if (result)
                {
                    npcCharacter.gameObject.GetComponent<NPCDialogueTrigger>().GiftDialogue("GiftLove");
                }
                Debug.Log("New affection" + npcCharacter.GetAffectionLevel());
                return true;
            }
        }
        return false;
    }
}
