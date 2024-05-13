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
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile plowedTile;

    // Start is called before the first frame update
    public void SetDiggableTiles()
    {
        digMap = GameObject.Find("Dig Map").GetComponent<Tilemap>();
        plantablesMap = GameObject.Find("Plantables Map").GetComponent<Tilemap>();
        if (digMap == null)
        {
            return;
        }
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
}
