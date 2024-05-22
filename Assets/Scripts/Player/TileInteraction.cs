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
    }

    public void UseItemOnTile()
    {
        if (tileManager != null)
        {
            Vector3Int position = tilePos;
            string tileName = tileManager.GetTileName(tilePos);
            if (!string.IsNullOrWhiteSpace(tileName) && inventory.GetInventory("Toolbar").selectedSlot != null)
            {
                // CheckItemSelected(tileName, inventory.toolbar.selectedSlot.itemName);
                Debug.Log(inventory.GetInventory("Toolbar").selectedSlot.itemName);
                CheckItemSelected(tileName, GameManager.instance.itemManager.GetItemByName(inventory.GetInventory("Toolbar").selectedSlot.itemName));
            }
        }
    }

    public void CheckHarvestable()
    {
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

    private void CheckItemSelected(string tileName, Item selectedItem)
    {
        if (selectedItem == null)
        {
            Debug.Log("Selected Item is null");
            return;
        }
        if (tileName == "Interactable" && selectedItem.data.itemName == "Shovel")
        {
            tileManager.SetDigTile(tilePos);
            return;
        }
        if ((tileName == "dugTile"))   //get item type
        {
            if (selectedItem is Plantable)
            {
                Plantable plantable = (Plantable) selectedItem;
                PlantableGrowth newPlant = new PlantableGrowth(tilePos, selectedItem.data.itemName, plantable.plantableData.growthTiles, plantable.plantableData.growthTime, plantable.plantableData.harvestItemName);
                if (!plantableGrowthDict.ContainsKey(tilePos))
                {
                    plantableGrowthDict.Add(tilePos, newPlant);
                    inventory.GetInventory("Toolbar").selectedSlot.RemoveItem();
                }
            }
            else if (selectedItem.data.itemName == "Watering Can")
            {
                tileManager.SetWateredTile(tilePos);
                if (plantableGrowthDict.ContainsKey(tilePos))
                {
                    plantableGrowthDict[tilePos].isWatered = true;
                }
            }

        }
        else if ((tileName == "wateredTile") && selectedItem is Plantable)   //get item type
        {
            Plantable plantable = (Plantable) selectedItem;
            PlantableGrowth newPlant = new PlantableGrowth(tilePos, selectedItem.data.itemName, plantable.plantableData.growthTiles, plantable.plantableData.growthTime, plantable.plantableData.harvestItemName);
            if (!plantableGrowthDict.ContainsKey(tilePos))
            {
                newPlant.isWatered = true;
                plantableGrowthDict.Add(tilePos, newPlant);
                inventory.GetInventory("Toolbar").selectedSlot.RemoveItem();
            }
        }
    }

    private void UpdateTileSelectorPos()
    {
        Vector3Int tilePos = new Vector3Int((int) Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y), 0);

        if (inventory.GetInventory("Toolbar").selectedSlot == null)
        {
            tileSelector.SetActive(false);
            return;
        }
        Item selectedItem = GameManager.instance.itemManager.GetItemByName(inventory.GetInventory("Toolbar").selectedSlot.itemName);
        if (inventory.GetInventory("Toolbar").selectedSlot.itemName == "Shovel" || inventory.GetInventory("Toolbar").selectedSlot.itemName == "Watering Can" || selectedItem is Plantable)
        {
            tileSelector.transform.localScale = new Vector2(1,1);
            tileSelector.SetActive(true);
            return;
        }
        else if (selectedItem is Placeable)
        {
            tileSelector.SetActive(true);
            tileSelector.transform.localScale = ((Placeable) selectedItem).placeableData.size;
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
            tileManager.SetDigTile(pos);
        }
        GameManager.instance.tileSave.IncrementDayOffMap();
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

    public void SetPlantableGrowthDict(Dictionary<Vector3Int, PlantableGrowth> newDict)
    {
        plantableGrowthDict = newDict;
        foreach (Vector3Int pos in plantableGrowthDict.Keys)
        {
            tileManager.SetDigTile(pos);
            plantableGrowthDict[pos].UpdateTileDisplay();
        }
    }

    public Dictionary<Vector3Int, PlantableGrowth> GetPlantableGrowthsDict()
    {
        return plantableGrowthDict;
    }

    public bool PlaceItem(GameObject placedItem, Vector2 size)
    {
        if (GameManager.instance.tileManager.CheckPlaceable(tilePos, size))
        {
            //place the item
            Vector3 calcedPos = new Vector3((tilePos.x + size.x/2), (tilePos.y + size.y/2), 0);
            Instantiate(placedItem, tilePos, Quaternion.identity);
            GameManager.instance.tileManager.Place(tilePos, size);

            Item selectedItem = GameManager.instance.itemManager.GetItemByName(inventory.GetInventory("Toolbar").selectedSlot.itemName);
            if (selectedItem is Chest)
            {
                GameManager.instance.player.inventory.AddChest(tilePos);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
