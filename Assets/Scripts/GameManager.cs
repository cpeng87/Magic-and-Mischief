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

    public Player player;

    public int activeMenuCount;
    private Stack<GameObject> activeMenus = new Stack<GameObject>();

    private List<Inventory.Slot> savedSlots;
    private string nextSpawnpoint;

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
        tileSave = GetComponent<TileSave>();

        player = FindObjectOfType<Player>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = FindObjectOfType<Player>();
        if (savedSlots != null)
        {
            player.inventory.GetInventoryByName("Backpack").SetInventorySlots(savedSlots);
        }
        tileManager.SetDiggableTiles();

        SpawnpointManager sm = FindObjectOfType<SpawnpointManager>();
        sm.OrganizeSpawnpoint();
        if (sm.CheckContainsKey(nextSpawnpoint))
        {
            player.transform.position = sm.GetPositionByName(nextSpawnpoint);
        }
        tileManager.LoadPlantablesMap();
    }

    public void SceneSwap(string sceneName, string spawnpointName)
    {
        savedSlots = player.inventory.GetInventoryByName("Backpack").slots;
        SceneManager.LoadScene(sceneName);
        tileSave.AddMapPlantables(SceneManager.GetActiveScene().name, player.ti.GetPlantableGrowthsDict());

        nextSpawnpoint = spawnpointName;
    }

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
