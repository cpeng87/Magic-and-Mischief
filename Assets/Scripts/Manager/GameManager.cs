using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemManager itemManager;
    public TileManager tileManager;
    public TimeManager timeManager;
    public TileSave tileSave;
    public UIManager uiManager;
    public DialogueManager dialogueManager;
    public MailManager mailManager;
    public SceneSwapManager sceneSwapManager;
    public NPCManager npcManager;
    public NotebookManager notebookManager;

    public Player player;

    public int activeMenuCount;
    private Stack<GameObject> activeMenus = new Stack<GameObject>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        itemManager = GetComponent<ItemManager>();
        tileManager = GetComponent<TileManager>();
        timeManager = GetComponent<TimeManager>();
        timeManager.Setup();
        tileSave = GetComponent<TileSave>();
        uiManager = GetComponent<UIManager>();
        dialogueManager = GetComponent<DialogueManager>();
        mailManager = GetComponent<MailManager>();
        sceneSwapManager = GetComponent<SceneSwapManager>();
        npcManager = GetComponent<NPCManager>();
        notebookManager = GetComponent<NotebookManager>();

        player = FindObjectOfType<Player>();
        // savedHealth = -1;
        // savedMana = -1;

        // SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UnityEngine.Time.timeScale = 1f;
        player = FindObjectOfType<Player>();
        MapManager mapManager = FindObjectOfType<MapManager>();

        sceneSwapManager.SceneLoad();
    }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     Time.timeScale = 1f;
    //     player = FindObjectOfType<Player>();

    //     MapManager mapManager = FindObjectOfType<MapManager>();

    //     if (player != null)
    //     {
    //         player.bm.SetBuffs(savedBuffs);
    //         if (savedHealth >= 0)
    //         {
    //             player.health.SetVal(savedHealth);
    //         }
    //         if (savedMana >= 0)
    //         {
    //             player.mana.SetVal(savedMana);
    //         }
    //         if (savedBackpack != null)
    //         {
    //             player.inventory.SetInventoryByName("Backpack", savedBackpack);
    //         }
    //         if (savedToolbar != null)
    //         {
    //             player.inventory.SetInventoryByName("Toolbar", savedToolbar);
    //         }
    //         player.pa.SetDirection(savedDir);
    //     }
    //     if (mapManager != null)
    //     {
    //         mapManager.SpawnCollectibles();
    //     }
    //     if (tileManager != null)
    //     {
    //         tileManager.SetMaps();
    //         tileManager.SetDiggableTiles();
    //         tileManager.LoadPlantablesMap();
    //     }
    //     if (savedChests != null && savedChests.ContainsKey(SceneManager.GetActiveScene().name))
    //     {
    //         player.inventory.chests = savedChests[SceneManager.GetActiveScene().name];
    //         player.inventory.LoadChests();
    //     }
    //     if (dialogueManager != null)
    //     {
    //         dialogueManager.Load();
    //     }

    //     SpawnpointManager sm = FindObjectOfType<SpawnpointManager>();
    //     if (sm != null)
    //     {
    //         sm.OrganizeSpawnpoint();
    //         if (sm.CheckContainsKey(nextSpawnpoint))
    //         {
    //             player.transform.position = sm.GetPositionByName(nextSpawnpoint);
    //         }
    //     }
    // }

    // public void SceneSwap(string sceneName, string spawnpointName)
    // {
    //     if (player != null)
    //     {
    //         savedBackpack = player.inventory.GetInventory("Backpack");
    //         savedToolbar = player.inventory.GetInventory("Toolbar");
    //         savedHealth = player.health.currVal;
    //         savedMana = player.mana.currVal;
    //         savedDir = player.pm.GetCurrDirection();
    //         tileSave.AddMapPlantables(SceneManager.GetActiveScene().name, player.ti.GetPlantableGrowthsDict());
    //         if (savedChests.ContainsKey(SceneManager.GetActiveScene().name))
    //         {
    //             savedChests[SceneManager.GetActiveScene().name] = player.inventory.GetChests();
    //         }
    //         else
    //         {
    //             savedChests.Add(SceneManager.GetActiveScene().name, player.inventory.GetChests());
    //         }
    //         savedBuffs = player.bm.activeBuffs;
    //     }
    //     // tileSave.AddMapPlantables(SceneManager.GetActiveScene().name, player.ti.GetPlantableGrowthsDict());
    //     SceneManager.LoadScene(sceneName);
    //     nextSpawnpoint = spawnpointName;
    // }

    public void PushActiveMenu(GameObject newMenu)
    {
        activeMenus.Push(newMenu);
        activeMenuCount += 1;
    }
    public void PopActiveMenu()
    {
        activeMenus.Pop();
        activeMenuCount -= 1;
        if (activeMenuCount < 0)
        {
            activeMenuCount = 0;
        }
    }
    public GameObject PeekActiveMenu()
    {
        if (activeMenuCount == 0)
        {
            return null;
        }
        return activeMenus.Peek();
    }
}
