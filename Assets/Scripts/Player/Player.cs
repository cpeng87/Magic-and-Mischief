using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryManager inventory;
    public PlayerSpell ps;
    public PlayerMovement pm;
    public PlayerAnimation pa;
    public TileInteraction ti;
    public BuffManager bm;

    public StatusBar health;
    public StatusBar mana;
    [SerializeField] private StatusBarUI healthbar;
    [SerializeField] private StatusBarUI manabar; 

    public int numInventorySlots;
    public int numToolbarSlots;
    public int numHealth;
    public int numMana;

    public int currency;
    private int maxCurrency = 999999;

    public GameObject mailCrow;
    private Animator mailCrowAnimator;

    private void Awake()
    {
        inventory = GetComponent<InventoryManager>();
        ps = GetComponent<PlayerSpell>();
        pm = GetComponent<PlayerMovement>();
        pa = GetComponent<PlayerAnimation>();
        ti = GetComponent<TileInteraction>();
        bm = GetComponent<BuffManager>();
        health = new StatusBar(numHealth, healthbar);
        mana = new StatusBar(numMana, manabar);

        MailEventHandler.OnMailChanged += MailAnimation;
        mailCrowAnimator = mailCrow.GetComponent<Animator>();
    }

    private void MailAnimation()
    {
        mailCrow.SetActive(true);
        mailCrowAnimator.SetTrigger("New Mail");
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;

        Vector2 spawnOffset = Random.insideUnitCircle * 2f;

        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);
        // droppedItem.rb.AddForce(spawnOffset * 2f, ForceMode2D.Impulse);
    }
    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }

    public bool AddCurrency(int addedVal)
    {
        if (currency + addedVal > maxCurrency)
        {
            return false;
        }
        currency += addedVal;
        CurrencyEventHandler.TriggerCurrencyChangedEvent();
        return true;
    }

    public bool SubtractCurrency(int subtractedVal)
    {
        if (currency < subtractedVal)
        {
            return false;
        }
        currency -= subtractedVal;
        CurrencyEventHandler.TriggerCurrencyChangedEvent();
        return true;
    }

    private void OnDestroy()
    {
        MailEventHandler.OnMailChanged -= MailAnimation;
    }
}
