using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int minutes = 0;
    public int hours = 0;
    public string currDay = "Monday";
    public float minutesPerSecond = 1.0f;

    private string[] days = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};
    private float elapsedTime = 0.0f;
    private int dayCounter = 0;

    void Update()
    {
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
            if (hours >= 24)
            {
                hours = 0;
                dayCounter = (dayCounter + 1) % 7;
                currDay = days[dayCounter];
                TimeEventHandler.TriggerDayChangedEvent();
            }
        }
    }
}

