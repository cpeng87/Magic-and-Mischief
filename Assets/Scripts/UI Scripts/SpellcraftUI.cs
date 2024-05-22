using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpellcraftUI : InteractUI
{
    public GameObject spellcraftPanel;
    public GameObject materialsPanel;
    public GameObject materialsListing;
    public TextMeshProUGUI spellnameText;
    public TextMeshProUGUI spellDescriptionText;
    public TextMeshProUGUI manaCostText;
    public Image spellImage;

    public Inventory inventory;
    public Inventory toolbar;

    public List<SpellData> spells = new List<SpellData>();

    private int currIndex;

    private void Awake()
    {
        spellcraftPanel.SetActive(false);
    }
    private void Start()
    {
        if (!GameManager.instance.player.inventory)
        {
            Debug.Log("Player inventory is null");
        }
        inventory = GameManager.instance.player.inventory.GetInventory("Backpack");
        toolbar = GameManager.instance.player.inventory.GetInventory("Toolbar");
    }

    // Start is called before the first frame update
    public override void ToggleUI()
    {
        if (!spellcraftPanel.activeSelf)
        {
            spellcraftPanel.SetActive(true);
            Time.timeScale = 0f;
            GameManager.instance.PushActiveMenu(this.gameObject);
            LoadPage();
        }
        else
        {   
            spellcraftPanel.SetActive(false);
            GameManager.instance.PopActiveMenu();
            if (GameManager.instance.activeMenuCount == 0)
            {
                Time.timeScale = 1f;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!spellcraftPanel.activeSelf || this.gameObject != GameManager.instance.PeekActiveMenu())
        {
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleUI();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (CheckNecessaryMaterials(spells[currIndex].ingredients))
            {
                CraftSpell(spells[currIndex].ingredients);
            }

        }
    }
    private void LoadPage()
    {
        if (spells.Count > 0)
        {
            currIndex = 0;
            spellnameText.text = spells[currIndex].itemName;
            spellDescriptionText.text = spells[currIndex].description;
            spellImage.sprite = spells[currIndex].icon;
            manaCostText.text = spells[currIndex].spellManaCost.ToString();
            LoadMaterials();
        }
    }
    private void LoadMaterials()
    {
        ClearMaterials();
        foreach (IngredientListing material in spells[currIndex].ingredients)
        {
            GameObject newMaterial = Instantiate(materialsListing, materialsPanel.transform);
            newMaterial.GetComponent<MaterialListingUI>().SetMaterialListing(material.listingData.itemName, material.quantity, material.listingData.icon);
        }
    }

    private void ClearMaterials()
    {
        // Loop through each child of the parentObject
        foreach (Transform child in materialsPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private bool CheckNecessaryMaterials(List<IngredientListing> itemsNeeded)
    {
        foreach (IngredientListing item in itemsNeeded)
        {
            if (!inventory.CheckInventoryForItemAndQuantity(item.listingData.itemName, item.quantity))
            {
                Debug.Log("Can't find: " + item.listingData.itemName);
                return false;
            }
        }
        return true;
    }
    private void CraftSpell(List<IngredientListing> itemsNeeded)
    {
        foreach (IngredientListing item in itemsNeeded)
        {
            inventory.SubtractItemAndQuantity(item.listingData.itemName, item.quantity);
        }
        inventory.Add(GameManager.instance.itemManager.GetItemByName(spells[currIndex].itemName));
    }
}
