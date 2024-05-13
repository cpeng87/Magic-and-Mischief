using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileSave : MonoBehaviour
{
    private string currMap;
    private Dictionary<string, Dictionary<Vector3Int, PlantableGrowth>> mapPlantableTiles =
        new Dictionary<string, Dictionary<Vector3Int, PlantableGrowth>>();
    // Start is called before the first frame update

    public void AddMapPlantables(string mapName, Dictionary<Vector3Int, PlantableGrowth> plantableTiles)
    {
        if (mapPlantableTiles.ContainsKey(mapName))
        {
            mapPlantableTiles[mapName] = plantableTiles;
        }
        else
        {
            mapPlantableTiles.Add(mapName, plantableTiles);
        }
    }

    // Update is called once per frame
    public void IncrementDayOffMap()
    {
        currMap = SceneManager.GetActiveScene().name;
        foreach (string map in mapPlantableTiles.Keys)
        {
            if (currMap != map)
            {
                Dictionary<Vector3Int, PlantableGrowth> mapTileDict = mapPlantableTiles[map];
                foreach (Vector3Int position in mapTileDict.Keys)
                {
                    mapTileDict[position].IncrementDay();
                }
            }
        }
    }
    public Dictionary<Vector3Int, PlantableGrowth> GetSavedMapTile(string mapName)
    {
        if (!mapPlantableTiles.ContainsKey(mapName))
        {
            return new Dictionary<Vector3Int, PlantableGrowth>();
        }
        return mapPlantableTiles[mapName];
    }
}
