using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RepairUI : InteractUI
{
    public Repairable toBeChanged;
    private RepairableData repairData;
    public GameObject materialsPanel;
    public GameObject repairPanel;
    public TextMeshProUGUI questionText;
    public GameObject materialsListing;

    public Inventory inventory;
    public List<IngredientListing> itemsNeeded;

    // Start is called before the first frame update
    void Start()
    {
        repairPanel.SetActive(false);
        inventory = GameManager.instance.player.inventory.GetInventory("Backpack");
    }

    public override void ToggleUI()
    {
        if (!repairPanel.activeSelf)
        {
            LoadRepairData();
            LoadMaterials();
            repairPanel.SetActive(true);
            Time.timeScale = 0f;
            GameManager.instance.PushActiveMenu(this.gameObject);
        }
        else
        {   
            repairPanel.SetActive(false);
            GameManager.instance.PopActiveMenu();
            if (GameManager.instance.activeMenuCount == 0)
            {
                Time.timeScale = 1f;
            }
            
        }
    }

    public void SetToBeChanged(GameObject interactedRepairable)
    {

        toBeChanged = interactedRepairable.GetComponent<Repairable>();
        LoadRepairData();
    }

    public void LoadRepairData()
    {
        repairData = toBeChanged.GetComponent<Repairable>().repairableData;
        itemsNeeded = repairData.ingredients;
    }

    private void LoadMaterials()
    {
        ClearMaterials();
        foreach (IngredientListing material in repairData.ingredients)
        {
            GameObject newMaterial = Instantiate(materialsListing, materialsPanel.transform);
            newMaterial.GetComponent<MaterialListingUI>().SetMaterialListing(material.listingData.itemName, material.quantity, material.listingData.icon);
        }
        UpdateText(toBeChanged.name);
    }

    private void ClearMaterials()
    {
        foreach (Transform child in materialsPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private bool CheckNecessaryMaterials()
    {
        foreach (IngredientListing item in itemsNeeded)
        {
            if (!inventory.CheckInventoryForItemAndQuantity(item.listingData.itemName, item.quantity))
            {
                Debug.Log(item.listingData.itemName);
                return false;
            }
        }
        return true;
    }
    public void Repair()
    {
        Debug.Log("Checking repair");
        if (CheckNecessaryMaterials())
        {
            Debug.Log("Necessary Materials Found");
            foreach (IngredientListing item in itemsNeeded)
            {
                inventory.SubtractItemAndQuantity(item.listingData.itemName, item.quantity);
            }
            toBeChanged.Repair();
            ToggleUI();
        }
    }
    public void UpdateText(string nameOfObject)
    {
        questionText.text = "Would you like to repair the " + nameOfObject + "?";
    }
}
