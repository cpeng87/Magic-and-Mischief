using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public SlotUI draggedSlot;
    public Image draggedIcon;
    public bool dragSingle;
    
    private Stack<Toggleable> activeMenus = new Stack<Toggleable>();
    private int activeMenuCount = 0;

    public Toggleable pauseMenu;
    public bool isDialogue;

    //0 for outer basic uis
    //100 for mouse items
    public int currSortingOrder = 0;

    public void Setup()
    {
        pauseMenu = GameObject.Find("Pause Menu").GetComponent<Toggleable>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activeMenuCount > 0)
            {
                activeMenus.Peek().ToggleUI();
            }
            else
            {
                if (pauseMenu != null)
                {
                    pauseMenu.ToggleUI();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            dragSingle = false;
        }
    }

    public void CloseTopMenu()
    {
        Toggleable topMenu = activeMenus.Peek();
        topMenu.ToggleUI();
    }

    public void PushActiveMenu(Toggleable newMenu)
    {
        activeMenus.Push(newMenu);
        currSortingOrder += 1;
        newMenu.gameObject.GetComponent<Canvas>().sortingOrder = currSortingOrder;
        activeMenuCount += 1;
    }
    public void PopActiveMenu()
    {
        activeMenus.Pop();
        activeMenuCount -= 1;
        currSortingOrder -= 1;
    }
    public Toggleable PeekActiveMenu()
    {
        if (activeMenuCount == 0)
        {
            return null;
        }
        return activeMenus.Peek();
    }

    public int GetActiveMenuCount()
    {
        return activeMenuCount;
    }

    public void StartDialogue()
    {
        UnityEngine.Time.timeScale = 0f;
        foreach (Toggleable menu in activeMenus)
        {
            menu.ToggleUI();
        }
        isDialogue = true;
    }

    public void EndDialogue()
    {
        //set is dialoguing
        UnityEngine.Time.timeScale = 1f;
        isDialogue = false;
    }
}
