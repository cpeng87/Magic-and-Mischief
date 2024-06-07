using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Date
{
    public int season;
    public int day;
    public int year;

    public Date(int season, int day, int year)
    {
        this.season = season;
        this.day = day;
        this.year = year;
    }

    public Date()
    {
        this.season = 0;
        this.day = 0;
        this.year = 1;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Date other = (Date)obj;
        return season == other.season && day == other.day && year == other.year;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + season.GetHashCode();
            hash = hash * 23 + day.GetHashCode();
            hash = hash * 23 + year.GetHashCode();
            return hash;
        }
    }

    public override string ToString()
    {
        string rtn = season + "/" + day + "/" + year;
        return rtn;
    }
}


public class MailManager : MonoBehaviour
{
    public List<MailData> mails = new List<MailData>();
    public Dictionary<Date, List<MailData>> mailByDate = new Dictionary<Date, List<MailData>>();
    public List<(MailData, bool)> activeMail = new List<(MailData, bool)>();
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
        Debug.Log(currDate.ToString());
        Debug.Log("Checking Mail");
        if (mailByDate.ContainsKey(currDate))
        {
            MailEventHandler.TriggerMailChangedEvent();
            Debug.Log("Mail changed event!");
            foreach (MailData mail in mailByDate[currDate])
            {
                activeMail.Add((mail, false));
            }
            mailByDate.Remove(currDate);
        }
    }
}
