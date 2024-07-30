using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailManager : MonoBehaviour
{
    public List<MailData> mails = new List<MailData>();
    private Dictionary<Date, List<MailData>> mailByDate = new Dictionary<Date, List<MailData>>();
    private List<(MailData, bool)> activeMail = new List<(MailData, bool)>();
    // Start is called before the first frame update
    void Start()
    {
        SetupDictionary();
        TimeEventHandler.OnDayChanged += CheckMail;
    }

    private void SetupDictionary()
    {
        foreach (MailData mail in mails)
        {
            if (mailByDate.ContainsKey(mail.date))
            {
                mailByDate[mail.date].Add(mail);
            }
            else
            {
                mailByDate.Add(mail.date, new List<MailData> { mail });
            }
        }
    }

    private void OnDestroy()
    {
        TimeEventHandler.OnDayChanged -= CheckMail;
    }

    private void CheckMail()
    {
        Date currDate = GameManager.instance.timeManager.date;
        if (currDate == null)
        {
            Debug.Log("currDate is null");
        }
        if (mailByDate.ContainsKey(currDate))
        {
            MailEventHandler.TriggerNewMailEvent();
            MailEventHandler.TriggerMailChangedEvent();
            foreach (MailData mail in mailByDate[currDate])
            {
                activeMail.Add((mail, false));
            }
            mailByDate.Remove(currDate);
        }
    }

    public void DeleteMail(int index)
    {
        activeMail.RemoveAt(index);
        MailEventHandler.TriggerMailChangedEvent();
    }

    public List<(MailData, bool)> GetActiveMail()
    {
        return activeMail;
    }
    public void SetMailReadStatus(int index, bool status)
    {
        var currentTuple = activeMail[index];
        activeMail[index] = (currentTuple.Item1, status);
    }
}
