using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public string[] mapCollectibles;
    public float probablityOfSpawn;
    public Tilemap spawnableTiles;
    private GameObject[] spawnedItems;

    public void SpawnCollectibles()
    {
        foreach(var position in spawnableTiles.cellBounds.allPositionsWithin)
        {
            if (spawnableTiles.HasTile(position))
            {
                float probabilityVal = Random.Range(0f, 1f);
                if (probabilityVal < probablityOfSpawn)
                {
                    int random = (int) Random.Range(0f, mapCollectibles.Length);
                    Vector3 spawnPosition = spawnableTiles.GetCellCenterWorld(position);
                    Instantiate(GameManager.instance.itemManager.GetItemByName(mapCollectibles[random]), spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}
