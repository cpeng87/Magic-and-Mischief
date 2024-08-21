using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : Toggleable
{
    // public GameObject display;

    void Start()
    {
        toggledDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleUI();
        }
    }

    // public void ToggleUI()
    // {
    //     if (!display.activeSelf)
    //     {
    //         display.SetActive(true);
    //         UnityEngine.Time.timeScale = 0f;
    //         GameManager.instance.PushActiveMenu(this.gameObject);
    //     }
    //     else
    //     {
    //         display.SetActive(false);
    //         GameManager.instance.PopActiveMenu();
    //         if (GameManager.instance.activeMenuCount == 0)
    //         {
    //             UnityEngine.Time.timeScale = 1f;
    //         }
    //     }
    // }
}
