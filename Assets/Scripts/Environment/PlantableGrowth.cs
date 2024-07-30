using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantableGrowth
{
    private TileManager tileManager;

    private Tile[] tiles;
    private int growthTime;
    private int timeBetweenGrowth;

    private Vector3Int position;
    private string itemName;
    private string harvestItemName;
    private Tile activeTile;
    private bool isFullyGrown = false;
    private bool isWatered;

    private int growthCounter;
    private int tileCounter = 0;

    public PlantableGrowth(Vector3Int position, string itemName, Tile[] tiles, int growthTime, string harvestItemName)
    {
        this.itemName = itemName;
        this.position = position;
        this.harvestItemName = harvestItemName;
        tileManager = GameManager.instance.tileManager;
        // timeBetweenGrowth = tiles.Length / growthTime;
        timeBetweenGrowth = growthTime / tiles.Length;
        Debug.Log("Time between growth: " + timeBetweenGrowth);
        tileManager.SetPlantablesTile(position, tiles[tileCounter]);
        this.tiles = tiles;
        this.growthTime = growthTime;
    }

    public void CheckAndUpdateGrowth()
    {
        if (isFullyGrown)
        {
            return;
        }
        if (growthCounter >= timeBetweenGrowth)  //update tile
        {
            Debug.Log("Growth counter: " + growthCounter);
            Debug.Log("Time between growth: " + timeBetweenGrowth);
            tileCounter++;
            if (tileCounter + 1 >= tiles.Length)
            {
                isFullyGrown = true;
            }

            tileManager.SetPlantablesTile(position, tiles[tileCounter]); // set at 0 for just seeds so far
            activeTile = tiles[tileCounter];
            growthCounter = 0;
        }
    }
    public void IncrementDay()
    {
        if (isWatered)
        {
            growthCounter++;
        }
        isWatered = false;
        CheckAndUpdateGrowth();
    }

    public void UpdateTileDisplay()
    {
        tileManager.SetPlantablesTile(position, tiles[tileCounter]);
    }

    public Vector3Int GetPosition()
    {
        return position;
    }
    public string GetHarvestItemName()
    {
        return harvestItemName;
    }

    public void SetIsWatered(bool state)
    {
        isWatered = state;
    }
    public bool GetIsFullyGrown()
    {
        return isFullyGrown;
    }

}
