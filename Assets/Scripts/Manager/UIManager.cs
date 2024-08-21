using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public SlotUI draggedSlot;
    public Image draggedIcon;
    public bool dragSingle;
    
    // private Stack<GameObject> activeMenus = new Stack<GameObject>();
    private Stack<Toggleable> activeMenus = new Stack<Toggleable>();
    private int activeMenuCount = 0;

    private bool isDialogue = false;


    void Update()
    {
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
        activeMenuCount += 1;
    }
    public void PopActiveMenu()
    {
        activeMenus.Pop();
        activeMenuCount -= 1;
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
            PopActiveMenu();
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
