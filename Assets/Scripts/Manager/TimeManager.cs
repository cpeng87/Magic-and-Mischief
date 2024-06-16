using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int minutes = 0;
    public int hours = 0;
    public string currDay = "Mon";
    public string currSeason = "Spr";
    public int year = 1;
    public Date date = new Date();

    public float minutesPerSecond = 1.0f;

    private string[] days = {"Mon", "Tue", "Wed", "Thr", "Fri", "Sat", "Sun"};
    private float elapsedTime = 0.0f;
    public int dayCounter = 0;

    private string[] seasons = {"Spr", "Sum", "Fall", "Win"};
    public int seasonCounter;

    private bool isPaused;

    void Update()
    {
        if (isPaused)
        {
            return;
        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= minutesPerSecond)
        {
            elapsedTime -= minutesPerSecond;
            UpdateTime();
        }
    }

    void UpdateTime()
    {
        minutes += 1;
        TimeEventHandler.TriggerMinuteChangedEvent();
        if (minutes >= 60)
        {
            minutes = 0;
            hours += 1;
            TimeEventHandler.TriggerHourChangedEvent();
            if (hours >= 23)
            {
                hours = 0;
                dayCounter += 1;
                currDay = days[(dayCounter) % 7];
                UpdateDate();
                TimeEventHandler.TriggerDayChangedEvent();

                if (dayCounter > 2)
                {
                    dayCounter = 0;
                    seasonCounter += 1;
                    currSeason = seasons[seasonCounter];
                    TimeEventHandler.TriggerSeasonChangedEvent();

                    if (seasonCounter > 4)
                    {
                        seasonCounter = 0;
                        year += 1;
                        TimeEventHandler.TriggerYearChangedEvent();
                    }
                }
            }
        }
    }

    private void UpdateDate()
    {
        this.date = new Date(seasonCounter, dayCounter, year);
    }

    public void PauseTime()
    {
        isPaused = true;
    }

    public void ResumeTime()
    {
        isPaused = false;
    }

}

