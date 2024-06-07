using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailUI : MonoBehaviour
{
    private MailData mailData;
    public GameObject mailPanel;
    public TextMeshProUGUI mailText;
    public TextMeshProUGUI senderText;
    // Start is called before the first frame update

    public void ToggleUI()
    {
        if (!mailPanel.activeSelf)
        {
            mailPanel.SetActive(true);
            Time.timeScale = 0f;
            GameManager.instance.PushActiveMenu(this.gameObject);
            SetupMail();
        }
        else
        {   
            mailPanel.SetActive(false);
            GameManager.instance.PopActiveMenu();
            if (GameManager.instance.activeMenuCount == 0)
            {
                Time.timeScale = 1f;
            }
            
        }
    }

    private void SetupMail()
    {
        mailText.text = mailData.content;
        senderText.text = mailData.sender;
    }

    private void SetMailData(MailData newMailData)
    {
        mailData = newMailData;
    }
}
