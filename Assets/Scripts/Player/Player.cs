using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryManager inventory;
    public PlayerSpell ps;
    public PlayerMovement pm;
    public PlayerAnimation pa;

    public StatusBar health;
    public StatusBar mana;
    [SerializeField] private StatusBarUI healthbar;
    [SerializeField] private StatusBarUI manabar; 

    public int numInventorySlots;
    public int numToolbarSlots;
    public int numHealth;
    public int numMana;

    [SerializeField] private GameObject tileSelector;
    private TileManager tileManager;

    private void Awake()
    {
        inventory = GetComponent<InventoryManager>();
        ps = GetComponent<PlayerSpell>();
        pm = GetComponent<PlayerMovement>();
        pa = GetComponent<PlayerAnimation>();
        health = new StatusBar(numHealth, healthbar);
        mana = new StatusBar(numMana, manabar);
        tileManager = GameManager.instance.tileManager;
    }

    private void Update()
    {   
        Vector3Int tilePos = new Vector3Int((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y), 0);

        if (inventory.backpack.selectedSlot != null && inventory.backpack.selectedSlot.itemName == "Shovel")
        {
            tileSelector.SetActive(true);
            tileSelector.transform.position = tilePos;
        }
        else 
        {
            tileSelector.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (tileManager != null)
            {
                Vector3Int position = tilePos;
                string tileName = tileManager.GetTileName(tilePos);
                if (!string.IsNullOrWhiteSpace(tileName))
                {
                    if (tileName == "Interactable" && inventory.backpack.selectedSlot.itemName == "Shovel")
                    {
                        tileManager.SetInteracted(tilePos);
                    }
                    // else if (tileName == "Interactable" && inventory.backpack.selectedSlot.itemName)   //get item type
                    // {
                    //     tileManager.SetI
                    // }
                }
            }
        }
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;

        Vector2 spawnOffset = Random.insideUnitCircle * 2f;

        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);
        // droppedItem.rb.AddForce(spawnOffset * 2f, ForceMode2D.Impulse);
    }
}
