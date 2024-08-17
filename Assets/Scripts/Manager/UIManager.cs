using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public SlotUI draggedSlot;
    public Image draggedIcon;
    public bool dragSingle;

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
}
