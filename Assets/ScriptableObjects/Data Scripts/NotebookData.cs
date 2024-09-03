using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntryType
{
    Image,
    PlainText,
    ItemList
}

[System.Serializable]
public class NotebookStage
{
    public EntryType entryType;
    public Sprite image;
    [TextAreaAttribute]
    public string text;
    public List<IngredientListing> itemList;
}

[CreateAssetMenu(fileName = "New Notebook Data", menuName = "Events/Notebook Data")]
public class NotebookData : ScriptableObject
{
    public string title;
    public List<NotebookStage> stages = new List<NotebookStage>();
    // public int index;
}
