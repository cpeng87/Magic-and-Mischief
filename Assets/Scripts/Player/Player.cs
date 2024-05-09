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

    private void Awake()
    {
        inventory = GetComponent<InventoryManager>();
        ps = GetComponent<PlayerSpell>();
        pm = GetComponent<PlayerMovement>();
        pa = GetComponent<PlayerAnimation>();
        health = new StatusBar(numHealth, healthbar);
        mana = new StatusBar(numMana, manabar);
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;

        Vector2 spawnOffset = Random.insideUnitCircle * 2f;

        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);
        // droppedItem.rb.AddForce(spawnOffset * 2f, ForceMode2D.Impulse);
    }
}
