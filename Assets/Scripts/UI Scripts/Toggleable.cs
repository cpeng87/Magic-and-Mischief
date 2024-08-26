using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Toggleable : MonoBehaviour
{
    public GameObject toggledDisplay;

    public virtual void ToggleUI()
    {
        if (GameManager.instance.uiManager.isDialogue)
        {
            return;
        }
        if (!toggledDisplay.gameObject.activeSelf)
        {
            toggledDisplay.gameObject.SetActive(true);
            UnityEngine.Time.timeScale = 0f;
            GameManager.instance.uiManager.PushActiveMenu(this);
            Setup();
        }
        else
        {   
            toggledDisplay.gameObject.SetActive(false);
            GameManager.instance.uiManager.PopActiveMenu();
            if (GameManager.instance.uiManager.GetActiveMenuCount() == 0)
            {
                UnityEngine.Time.timeScale = 1f;
            }
        }
    }

    public virtual void Setup() {}
}

