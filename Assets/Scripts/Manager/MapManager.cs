using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class MapSpawnable
{
    public float probability;
    public string itemName;
}

public class MapManager : MonoBehaviour
{
    public List<MapSpawnable> mapCollectibles = new List<MapSpawnable>();
    public float probablityOfSpawn;
    public Tilemap spawnableTiles;
    private List<Item> spawnedItems = new List<Item>();

    public void SpawnCollectibles()
    {
        NormalizeProbabilities();
        foreach(var position in spawnableTiles.cellBounds.allPositionsWithin)
        {
            if (spawnableTiles.HasTile(position))
            {
                float probabilityVal = Random.Range(0f, 1f);
                if (probabilityVal < probablityOfSpawn)
                {
                    // calculate what is being spawned.
                    int random = (int) Random.Range(0f, 100f);

                    string spawnedItem = GetRandomItemByProbability();
                    Vector3 spawnPosition = spawnableTiles.GetCellCenterWorld(position);
                    Vector3 randomizedOffset = new Vector3(Random.Range(0f, 0.5f), Random.Range(0f, 0.5f), 0);
                    spawnPosition = spawnPosition + randomizedOffset;
                    Item newItem = Instantiate(GameManager.instance.itemManager.GetItemByName(spawnedItem), spawnPosition, Quaternion.identity);
                    SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sortingLayerName = "Spawned Items"; // Set the sorting layer name
                    }
                    spawnedItems.Add(newItem);
                }
            }
        }
    }

    public List<(string, Vector3)> ExportSpawnedItems()
    {
        List<(string, Vector3)> rtn = new List<(string, Vector3)>();
        foreach (Item item in spawnedItems)
        {
            if (item != null)
            {
                rtn.Add((item.GetComponent<Item>().data.itemName, item.transform.position));
            }
        }
        return rtn;
    }
    public void SpawnCollectibles(List<(string, Vector3)> itemsImport)
    {
        foreach ((string, Vector3) item in itemsImport)
        {
            Item newItem = Instantiate(GameManager.instance.itemManager.GetItemByName(item.Item1), item.Item2, Quaternion.identity);
            spawnedItems.Add(newItem);
        }
    }

    private void NormalizeProbabilities()
    {
        float totalProbability = 0f;

        foreach (MapSpawnable item in mapCollectibles)
        {
            totalProbability += item.probability;
        }

        for (int i = 0; i < mapCollectibles.Count; i++)
        {
            mapCollectibles[i].probability /= totalProbability;
        }
    }

    private string GetRandomItemByProbability()
    {
        float randomValue = Random.Range(0f, 1f);
        float cumulativeProbability = 0f;

        foreach (var item in mapCollectibles)
        {
            cumulativeProbability += item.probability;
            if (randomValue < cumulativeProbability)
            {
                return item.itemName;
            }
        }
        return mapCollectibles[mapCollectibles.Count - 1].itemName;
    }
}
