using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MailUI : MonoBehaviour
{
    // private MailData mailData;
    public GameObject mailPanel;
    public TextMeshProUGUI mailText;
    public TextMeshProUGUI senderText;
    public TextMeshProUGUI noMailText;

    public GameObject mailboxListingPrefab;
    public GameObject mailboxPanel;
    public GameObject mailboxDisplay;

    public GameObject mailOpenedPanel;
    // Start is called before the first frame update

    private void Start()
    {
        mailPanel.SetActive(false);
        mailOpenedPanel.SetActive(false);
        // mailboxPanel.SetActive(true);
        mailboxDisplay.SetActive(true);
        MailEventHandler.OnMailChanged += SetupMailbox;
    }

    private void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            ToggleUI();
        }
    }

    public void ToggleUI()
    {
        if (!mailPanel.activeSelf)
        {
            mailPanel.SetActive(true);
            UnityEngine.Time.timeScale = 0f;
            GameManager.instance.PushActiveMenu(this.gameObject);
            SetupMailbox();
        }
        else
        {   
            mailPanel.SetActive(false);
            GameManager.instance.PopActiveMenu();
            if (GameManager.instance.activeMenuCount == 0)
            {
                UnityEngine.Time.timeScale = 1f;
            }
        }
    }

    private void SetupMailbox()
    {
        mailboxPanel.SetActive(true);
        ClearMailbox();

        List<(MailData, bool)> activeMail = GameManager.instance.mailManager.GetActiveMail();

        if (activeMail.Count == 0)
        {
            noMailText.text = "No Mail";
        }
        else
        {
            noMailText.text = "";
        }

        int counter = 0;
        foreach ((MailData, bool) mailInfo in activeMail)
        {
            GameObject newMail = Instantiate(mailboxListingPrefab, mailboxPanel.transform);
            MailboxListingUI mailboxListing = newMail.GetComponent<MailboxListingUI>();

            mailboxListing.mailID = counter;

            if (mailboxListing != null)
            {
                mailboxListing.SetMailStrings(mailInfo.Item1.date.ToString(), mailInfo.Item1.subject, mailInfo.Item1.sender);
                if (mailInfo.Item2)
                {
                    mailboxListing.SetRead();
                }

                Button button = newMail.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(() => OpenMail(mailboxListing));
                }
            }
            else
            {
                Debug.LogWarning("MailboxListing component not found on the instantiated prefab.");
            }

            counter += 1;
        }
    }

    private void ClearMailbox()
    {
        foreach (Transform mail in mailboxPanel.transform)
        {
            Destroy(mail.gameObject);
        }
    }

    private void SetupMail(MailData mailData)
    {
        mailText.text = mailData.content;
        senderText.text = mailData.sender;
    }

    // private void SetMailData(MailData newMailData)
    // {
    //     mailData = newMailData;
    // }

    private bool CollectMailItems(MailData mailData)
    {
        bool giftResult = true;
        bool moneyResult = true;
        if (mailData.hasGift)
        {
            // giftResult = GameManager.instance.player.inventory.GetInventory("Backpack").Add(mailData.gift);
            giftResult = GameManager.instance.player.inventory.Add("Backpack", mailData.gift);
        }
        if (mailData.hasMoney)
        {
            moneyResult = GameManager.instance.player.AddCurrency(mailData.money);
        }
        return (giftResult && moneyResult);
    }

    private void OnDestroy()
    {
        MailEventHandler.OnMailChanged -= SetupMailbox;
    }

    public void OpenMail(MailboxListingUI clickedMail)
    {
        //setup actual ui elements
        mailboxDisplay.SetActive(false);

        mailOpenedPanel.SetActive(true);

        List<(MailData, bool)> activeMail = GameManager.instance.mailManager.GetActiveMail();
        MailData mailData = activeMail[clickedMail.mailID].Item1;
        SetupMail(mailData);

        bool success = false;
        if (!activeMail[clickedMail.mailID].Item2)
        {
            success = CollectMailItems(mailData);
        }

        //set mail as read
        if (success)
        {
            // activeMail[clickedMail.mailID] = (activeMail[clickedMail.mailID].Item1, true);
            GameManager.instance.mailManager.SetMailReadStatus(clickedMail.mailID, true);
        }
    }

    public void ReturnToMailbox()
    {
        mailOpenedPanel.SetActive(false);
        mailboxDisplay.SetActive(true);
        SetupMailbox();
    }
}
