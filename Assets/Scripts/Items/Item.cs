using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // [HideInInspector]
    public ItemData data;
    public void SetData(ItemData newData)
    {
        data = newData;
    }
    //returns true if item is used up, false if no
    public virtual bool Use()
    {
        return false;
    }
}
