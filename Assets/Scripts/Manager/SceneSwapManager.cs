using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapManager : MonoBehaviour
{
    private Inventory savedBackpack;
    private Inventory savedToolbar;
    private int savedHealth;
    private int savedMana;
    public Dictionary<string, Dictionary<Vector3Int, Inventory>> savedChests = new Dictionary<string, Dictionary<Vector3Int, Inventory>>();
    private string nextSpawnpoint;
    private Vector3 savedDir;
    //remaining, total time
    public List<Buff> savedBuffs = new List<Buff>();

    private void Awake()
    {
        savedHealth = -1;
        savedMana = -1;
    }

    public void SceneLoad()
    {
        UnityEngine.Time.timeScale = 1f;
        Player player = FindObjectOfType<Player>();

        MapManager mapManager = FindObjectOfType<MapManager>();

        player = GameManager.instance.player;

        if (player != null)
        {
            player.bm.SetBuffs(savedBuffs);
            if (savedHealth >= 0)
            {
                player.health.SetVal(savedHealth);
            }
            if (savedMana >= 0)
            {
                player.mana.SetVal(savedMana);
            }
            if (savedBackpack != null)
            {
                player.inventory.SetInventoryByName("Backpack", savedBackpack);
            }
            if (savedToolbar != null)
            {
                player.inventory.SetInventoryByName("Toolbar", savedToolbar);
            }
            player.pa.SetDirection(savedDir);
            SpawnpointManager sm = FindObjectOfType<SpawnpointManager>();
            if (sm != null)
            {
                sm.OrganizeSpawnpoint();
                if (sm.CheckContainsKey(nextSpawnpoint))
                {
                    player.transform.position = sm.GetPositionByName(nextSpawnpoint);
                }
            }
        }
        if (mapManager != null)
        {
            mapManager.SpawnCollectibles();
        }
        if (GameManager.instance.tileManager != null)
        {
            GameManager.instance.tileManager.SetMaps();
            GameManager.instance.tileManager.SetDiggableTiles();
            GameManager.instance.tileManager.LoadPlantablesMap();
        }
        if (savedChests != null && savedChests.ContainsKey(SceneManager.GetActiveScene().name))
        {
            player.inventory.chests = savedChests[SceneManager.GetActiveScene().name];
            player.inventory.LoadChests();
        }
        if (GameManager.instance.dialogueManager != null)
        {
            GameManager.instance.dialogueManager.Load();
        }

        if (GameManager.instance.npcManager != null)
        {
            GameManager.instance.npcManager.LoadInNPCs();
        }
    }

    public void SceneSwap(string sceneName, string spawnpointName)
    {
        Player player = GameManager.instance.player;
        if (player != null)
        {
            savedBackpack = player.inventory.GetInventory("Backpack");
            savedToolbar = player.inventory.GetInventory("Toolbar");
            savedHealth = player.health.currVal;
            savedMana = player.mana.currVal;
            savedDir = player.pm.GetCurrDirection();
            GameManager.instance.tileSave.AddMapPlantables(SceneManager.GetActiveScene().name, player.ti.GetPlantableGrowthsDict());
            if (savedChests.ContainsKey(SceneManager.GetActiveScene().name))
            {
                savedChests[SceneManager.GetActiveScene().name] = player.inventory.GetChests();
            }
            else
            {
                savedChests.Add(SceneManager.GetActiveScene().name, player.inventory.GetChests());
            }
            savedBuffs = player.bm.activeBuffs;
        }
        // tileSave.AddMapPlantables(SceneManager.GetActiveScene().name, player.ti.GetPlantableGrowthsDict());
        SceneManager.LoadScene(sceneName);
        nextSpawnpoint = spawnpointName;
    }
}