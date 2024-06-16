using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MailboxListingUI : MonoBehaviour
{
    public int mailID;

    public TextMeshProUGUI dateText;
    public TextMeshProUGUI topicText;
    public TextMeshProUGUI senderText;

    public Image display;

    public void SetMailStrings(string date, string topic, string sender)
    {
        dateText.text = date;
        topicText.text = topic;
        senderText.text = sender;
    }
    public void SetRead()
    {
        display.color = new Color(0,0,0,150);
    }
    public void DeleteMail()
    {
        GameManager.instance.mailManager.DeleteMail(mailID);
    }
}
