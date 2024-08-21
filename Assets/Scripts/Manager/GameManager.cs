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

    // public int activeMenuCount;
    // private Stack<GameObject> activeMenus = new Stack<GameObject>();

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

    // public void PushActiveMenu(GameObject newMenu)
    // {
    //     activeMenus.Push(newMenu);
    //     activeMenuCount += 1;
    // }
    // public void PopActiveMenu()
    // {
    //     activeMenus.Pop();
    //     activeMenuCount -= 1;
    //     if (activeMenuCount < 0)
    //     {
    //         activeMenuCount = 0;
    //     }
    // }
    // public GameObject PeekActiveMenu()
    // {
    //     if (activeMenuCount == 0)
    //     {
    //         return null;
    //     }
    //     return activeMenus.Peek();
    // }
}
