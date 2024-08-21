using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotoUI : Toggleable
{
    public Image photo;
    public TextMeshProUGUI text;
    public List<PhotoData> photos;
    // public GameObject photoPanel;
    private int index;

    private void Start()
    {
        toggledDisplay.SetActive(false);
        index = 0;
    }

    // Update is called once per frame
    // public override void ToggleUI()
    // {
    //     if (!photoPanel.activeSelf)
    //     {
    //         photoPanel.SetActive(true);
    //         UnityEngine.Time.timeScale = 0f;
    //         GameManager.instance.uiManager.PushActiveMenu(this);
    //         LoadPage();
    //     }
    //     else
    //     {   
    //         photoPanel.SetActive(false);
    //         GameManager.instance.uiManager.PopActiveMenu();
    //         if (GameManager.instance.uiManager.GetActiveMenuCount() == 0)
    //         {
    //             UnityEngine.Time.timeScale = 1f;
    //         }
    //     }
    // }

    public override void Setup()
    {
        if (photos.Count > index)
        {
            photo.sprite = photos[index].photo;
            text.text = photos[index].description;
        }
        else
        {
            Debug.Log("Index out of bounds.");
        }
    }

    public void NextPage()
    {
        if (photos.Count > index)
        {
            index += 1;
            Setup();
        }
    }
    public void PreviousPage()
    {
        if (index > 0)
        {
            index -= 1;
            Setup();
        }
    }
}
