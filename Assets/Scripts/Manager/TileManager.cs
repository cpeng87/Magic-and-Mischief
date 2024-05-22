using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour
{
    // [SerializeField] private Plantable[] plantables;
    private Tilemap digMap;
    private Tilemap plantablesMap;
    private Tilemap unplaceableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile plowedTile;
    [SerializeField] private Tile wateredTile;

    // Start is called before the first frame update
    public void SetMaps()
    {
        if (GameObject.Find("Dig Map") != null)
        {
            digMap = GameObject.Find("Dig Map").GetComponent<Tilemap>();
        }
        if (GameObject.Find("Plantables Map") != null)
        {
            plantablesMap = GameObject.Find("Plantables Map").GetComponent<Tilemap>();
        }
        if (GameObject.Find("Unplaceable Map") != null)
        {
            unplaceableMap = GameObject.Find("Unplaceable Map").GetComponent<Tilemap>();
        }
        // plantablesMap = GameObject.Find("Plantables Map").GetComponent<Tilemap>();
        // unplaceableMap = GameObject.Find("Unplaceable Map").GetComponent<Tilemap>();
        // digMap = GameObject.Find("Dig Map").GetComponent<Tilemap>();
    }

    public void SetDiggableTiles()
    {
        if (GameObject.Find("Dig Map") == null)
        {
            return;
        }
        plantablesMap = GameObject.Find("Plantables Map").GetComponent<Tilemap>();
        unplaceableMap = GameObject.Find("Unplaceable Map").GetComponent<Tilemap>();
        digMap = GameObject.Find("Dig Map").GetComponent<Tilemap>();

        // if (digMap == null)
        // {
        //     return;
        // }
        // set interactable tiles to invisible one
        foreach(var position in digMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = digMap.GetTile(position);

            if (tile != null && tile.name == "Interactable_Visible")
            {
                digMap.SetTile(position, hiddenInteractableTile);
            }
        }
    }

    public bool IsDiggable(Vector3Int position)
    {
        TileBase tile = digMap.GetTile(position);
        if (tile != null)
        {
            if (tile.name == "Interactable")
            {
                return true;
            }
        }
        return false;
    }

    public void SetDigTile(Vector3Int position)
    {
        digMap.SetTile(position, plowedTile);
    }
    public void SetWateredTile(Vector3Int position)
    {
        digMap.SetTile(position, wateredTile);
    }
    public void SetPlantablesTile(Vector3Int position, Tile newTile)
    {
        plantablesMap.SetTile(position, newTile);
    }
    public void SetPlantablesTileNull(Vector3Int position)
    {
        plantablesMap.SetTile(position, null);
    }
    public string GetTileName(Vector3Int position)
    {
        if (digMap != null)
        {
            TileBase tile = digMap.GetTile(position);
            if (tile != null)
            {
                return tile.name;
            }
        }
        return null;
    }
    public void LoadPlantablesMap()
    {
        GameManager.instance.player.GetComponent<TileInteraction>().SetPlantableGrowthDict(GameManager.instance.tileSave.GetSavedMapTile(SceneManager.GetActiveScene().name));
    }
    public bool CheckPlaceable(Vector3Int position, Vector2 size)
    {
        for (int i = position.x - (int) size.x - 1; i <= position.x; i++)
        {
            for (int j = position.y - (int) size.y - 1; j <= position.y; j++)
            {
                Vector3Int currPos = new Vector3Int(i, j, 0);
                if (unplaceableMap.GetTile(currPos) != null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void Place(Vector3Int position, Vector2 size)
    {
        for (int i = position.x - (int) size.x - 1; i <= position.x; i++)
        {
            for (int j = position.y - (int) size.y - 1; j <= position.y; j++)
            {
                Vector3Int currPos = new Vector3Int(i, j, 0);
                unplaceableMap.SetTile(position, plowedTile);
            }
        }
    }
}
