using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotoUI : InteractUI
{
    public Image photo;
    public TextMeshProUGUI text;
    public List<PhotoData> photos;
    public GameObject photoPanel;
    private int index;

    private void Start()
    {
        photoPanel.SetActive(false);
        index = 0;
    }

    // Update is called once per frame
    public override void ToggleUI()
    {
        if (!photoPanel.activeSelf)
        {
            photoPanel.SetActive(true);
            UnityEngine.Time.timeScale = 0f;
            GameManager.instance.PushActiveMenu(this.gameObject);
            LoadPage();
        }
        else
        {   
            photoPanel.SetActive(false);
            GameManager.instance.PopActiveMenu();
            if (GameManager.instance.activeMenuCount == 0)
            {
                UnityEngine.Time.timeScale = 1f;
            }
            
        }
    }
    private void LoadPage()
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
            LoadPage();
        }
    }
    public void PreviousPage()
    {
        if (index > 0)
        {
            index -= 1;
            LoadPage();
        }
    }
}
