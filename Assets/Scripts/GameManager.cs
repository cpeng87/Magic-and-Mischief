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

    public Player player;

    public int activeMenuCount;
    private Stack<GameObject> activeMenus = new Stack<GameObject>();

    private List<Inventory.Slot> savedSlots;

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

        player = FindObjectOfType<Player>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // itemManager = GetComponent<ItemManager>();
        // tileManager = GetComponent<TileManager>();
        // timeManager = GetComponent<TimeManager>();

        player = FindObjectOfType<Player>();
        if (savedSlots != null)
        {
            player.inventory.GetInventoryByName("Backpack").SetInventorySlots(savedSlots);
        }
    }

    public void SceneSwap(string sceneName)
    {
        savedSlots = player.inventory.GetInventoryByName("Backpack").slots;
        SceneManager.LoadScene(sceneName);
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
