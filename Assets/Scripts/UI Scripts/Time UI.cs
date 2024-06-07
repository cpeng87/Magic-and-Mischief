using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI hourText;
    public TextMeshProUGUI minuteText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI seasonText;
    private TimeManager tm;

    // Start is called before the first frame update
    void Start()
    {
        TimeEventHandler.OnMinuteChanged += RefreshMinute;
        TimeEventHandler.OnHourChanged += RefreshHour;
        TimeEventHandler.OnDayChanged += RefreshDay;
        TimeEventHandler.OnSeasonChanged += RefreshSeason;
        tm = GameManager.instance.timeManager;

        // Initialize time UI
        RefreshMinute();
        RefreshHour();
        RefreshDay();
        RefreshSeason();
    }

    private void RefreshMinute()
    {
        minuteText.text = tm.minutes.ToString("00");
    }

    private void RefreshHour()
    {
        hourText.text = tm.hours.ToString("00");
    }

    private void RefreshDay()
    {
        dayText.text = tm.currDay;
    }
    private void RefreshSeason()
    {
        seasonText.text = tm.currSeason;
    }
}
