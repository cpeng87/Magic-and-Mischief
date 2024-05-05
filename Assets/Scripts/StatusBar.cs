using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar
{
    public int maxVal;
    public int currVal;
    public StatusBarUI statusBarUI;

    public StatusBar(int maxVal, StatusBarUI statusBarUI)
    {
        this.maxVal = maxVal;
        this.currVal = maxVal;
        this.statusBarUI = statusBarUI;
        this.statusBarUI.SetMaxHealth(maxVal);
    }

    public void RestoreToMaxVal()
    {
        currVal = maxVal;
        statusBarUI.SetMaxHealth(maxVal);
    }
    public void AddVal(int addVal)
    {
        currVal += addVal;
        statusBarUI.SetHealth(currVal);
    }
    public void SetVal(int newVal)
    {
        currVal = newVal;
        statusBarUI.SetHealth(currVal);
    }
    public void SubtractVal(int valToSubtract)
    {
        currVal -= valToSubtract;
        if (currVal < 0)
        {
            currVal = 0;
        }
        statusBarUI.SetHealth(currVal);
    }
}
