using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    [SerializeField] private GameObject tileSelector;
    private TileManager tileManager;
    public InventoryManager inventory;
    private Vector3Int tilePos;

    private Dictionary<Vector3Int, PlantableGrowth> plantableGrowthDict= new Dictionary<Vector3Int, PlantableGrowth>();

    // Start is called before the first frame update
    void Awake()
    {
        inventory = GetComponent<InventoryManager>();
        tileManager = GameManager.instance.tileManager;
        // tileSelector = GameManager.instance.player.
        tileSelector.SetActive(false);
        tilePos = new Vector3Int((int) Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y), 0);
    }

    void Start()
    {
        InventoryEventHandler.OnInventoryChanged += UpdateTileSelectorPos;
        InventoryEventHandler.OnSelectedSlotChanged += UpdateTileSelectorPos;
        TimeEventHandler.OnDayChanged += UpdateDayCounters;
    }

    // Update is called once per frame
    void Update()
    {
        tilePos = new Vector3Int((int) Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y), 0);
        tileSelector.transform.position = tilePos;

        if (Input.GetMouseButtonDown(0))
        {
            if (tileManager != null)
            {
                Vector3Int position = tilePos;
                string tileName = tileManager.GetTileName(tilePos);
                if (!string.IsNullOrWhiteSpace(tileName))
                {
                    CheckItemSelected(tileName, inventory.backpack.selectedSlot.itemName);
                }
            }
        }
    }

    private void CheckItemSelected(string tileName, string itemName)
    {
        if (tileName == "Interactable" && itemName == "Shovel")
        {
            tileManager.SetDigTile(tilePos);
            return;
        }
        Item selectedItem = GameManager.instance.itemManager.GetItemByName(inventory.backpack.selectedSlot.itemName);
        if (tileName == "Interacted_Tile" && selectedItem is Plantable)   //get item type
        {
            Plantable plantable = (Plantable) selectedItem;
            PlantableGrowth newPlant = new PlantableGrowth(tilePos, itemName, plantable.plantableData.growthTiles, plantable.plantableData.growthTime, plantable.plantableData.harvestItemName);
            if (!plantableGrowthDict.ContainsKey(tilePos))
            {
                plantableGrowthDict.Add(tilePos, newPlant);
                inventory.backpack.selectedSlot.RemoveItem();
            }
        }
        if (plantableGrowthDict.ContainsKey(tilePos))
        {
            if (plantableGrowthDict[tilePos].isFullyGrown)
            {
                HarvestCrop(plantableGrowthDict[tilePos]);
                plantableGrowthDict.Remove(tilePos);
                tileManager.SetPlantablesTileNull(tilePos);
            }
        }
    }

    private void UpdateTileSelectorPos()
    {
        Vector3Int tilePos = new Vector3Int((int) Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y), 0);

        if (inventory.backpack.selectedSlot == null)
        {
            tileSelector.SetActive(false);
            return;
        }
        else if (inventory.backpack.selectedSlot.itemName == "Shovel" || GameManager.instance.itemManager.GetItemByName(inventory.backpack.selectedSlot.itemName) is Plantable)
        {
            tileSelector.SetActive(true);
            return;
        }
        else
        {
            tileSelector.SetActive(false);
            return;
        }
    }

    private void UpdateDayCounters()
    {
        foreach (Vector3Int pos in plantableGrowthDict.Keys)
        {
            plantableGrowthDict[pos].IncrementDay();
        }
    }

    private void HarvestCrop(PlantableGrowth plantableGrowth)
    {
        Item plant = GameManager.instance.itemManager.GetItemByName(plantableGrowth.harvestItemName);
        Item harvestItem = Instantiate(plant, plantableGrowth.position, Quaternion.identity);
    }

    void OnDestroy() {
        InventoryEventHandler.OnInventoryChanged -= UpdateTileSelectorPos;
        InventoryEventHandler.OnSelectedSlotChanged -= UpdateTileSelectorPos;
        TimeEventHandler.OnDayChanged -= UpdateDayCounters;
    }
}
