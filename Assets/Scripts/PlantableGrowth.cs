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

    public Vector3Int position;
    public string itemName;
    public string harvestItemName;
    public Tile activeTile;
    public bool isFullyGrown = false;

    private int dayCounter;
    private int tileCounter = 0;

    // Start is called before the first frame update
    public PlantableGrowth(Vector3Int position, string itemName, Tile[] tiles, int growthTime, string harvestItemName)
    {
        this.itemName = itemName;
        this.position = position;
        this.harvestItemName = harvestItemName;
        tileManager = GameManager.instance.tileManager;
        timeBetweenGrowth = tiles.Length / growthTime;
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
        if (dayCounter >= timeBetweenGrowth)  //update tile
        {
            tileCounter++;
            if (tileCounter + 1 >= tiles.Length)
            {
                isFullyGrown = true;
            }

            tileManager.SetPlantablesTile(position, tiles[tileCounter]); // set at 0 for just seeds so far
            activeTile = tiles[tileCounter];
            dayCounter = 0;
        }
    }
    public void IncrementDay()
    {
        dayCounter++;
        CheckAndUpdateGrowth();
    }

    public void UpdateTileDisplay()
    {
        tileManager.SetPlantablesTile(position, tiles[tileCounter]);
    }
}
