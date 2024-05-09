using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Plantable[] plantables;
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tilemap plantablesMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile plowedTile;

    // Start is called before the first frame update
    void Start()
    {
        // set interactable tiles to invisible one
        foreach(var position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);

            if (tile != null && tile.name == "Interactable_Visible")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            }
        }
    }

    public bool IsInteractable(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);
        if (tile != null)
        {
            if (tile.name == "Interactable")
            {
                return true;
            }
        }
        return false;
    }

    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, plowedTile);
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
        if (interactableMap != null)
        {
            TileBase tile = interactableMap.GetTile(position);
            if (tile != null)
            {
                return tile.name;
            }
        }
        return null;
    }
}
