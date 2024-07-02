using UnityEngine;

public class Date
{
    private string[] days = {"Mon", "Tue", "Wed", "Thr", "Fri", "Sat", "Sun"};
    private string[] seasons = {"Spr", "Sum", "Fall", "Win"};
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

    public string DayToString()
    {
        return days[day];
    }

    public string SeasonToString()
    {
        return seasons[season];
    }

    public void IncrementDate()
    {
        day += 1;
        TimeEventHandler.TriggerDayChangedEvent();
        if (day > 30)
        {
            day = 0;
            season += 1;
            TimeEventHandler.TriggerSeasonChangedEvent();
            if (season > 4)
            {
                season = 0;
                year += 1;
                TimeEventHandler.TriggerYearChangedEvent();
            }
        }
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

public class GameTime
{
    public int hour;
    public int minute;

    public GameTime()
    {
        hour = 0;
        minute = 0;
    }

    public GameTime(int hour, int minute)
    {
        this.hour = hour;
        this.minute = minute;
    }

    public bool IncrementTime()
    {
        minute += 1;
        TimeEventHandler.TriggerMinuteChangedEvent();
        if (minute > 60)
        {
            minute = 0;
            hour += 1;
            TimeEventHandler.TriggerHourChangedEvent();
            if (hour > 23)
            {
                hour = 0;
                return true;
            }
        }
        return false;
    }

    public string MinutesToString()
    {
        return minute.ToString("00");
    }

    public string HoursToString()
    {
        return hour.ToString("00");
    }
}

public class TimeManager : MonoBehaviour
{
    public Date date;
    public GameTime gameTime;

    public float minutesPerSecond = 1f;

    private float elapsedTime = 0.0f;

    private bool isPaused;

    public void Setup()
    {
        date = new Date(0,0,1);
        gameTime = new GameTime(0,0);
    }

    void Update()
    {
        if (isPaused)
        {
            return;
        }
        elapsedTime += UnityEngine.Time.deltaTime;
        if (elapsedTime >= minutesPerSecond)
        {
            elapsedTime -= minutesPerSecond;
            UpdateTime();
        }
    }

    void UpdateTime()
    {
        bool incrementDay = gameTime.IncrementTime();
        if (incrementDay)
        {
            date.IncrementDate();
        }
    }

    private void UpdateDate()
    {
        date.IncrementDate();
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



// using UnityEngine;

// public class Date
// {
//     public int season;
//     public int day;
//     public int year;

//     public Date(int season, int day, int year)
//     {
//         this.season = season;
//         this.day = day;
//         this.year = year;
//     }

//     public Date()
//     {
//         this.season = 0;
//         this.day = 0;
//         this.year = 1;
//     }

//     public override bool Equals(object obj)
//     {
//         if (obj == null || GetType() != obj.GetType())
//             return false;

//         Date other = (Date)obj;
//         return season == other.season && day == other.day && year == other.year;
//     }

//     public override int GetHashCode()
//     {
//         unchecked
//         {
//             int hash = 17;
//             hash = hash * 23 + season.GetHashCode();
//             hash = hash * 23 + day.GetHashCode();
//             hash = hash * 23 + year.GetHashCode();
//             return hash;
//         }
//     }

//     public override string ToString()
//     {
//         string rtn = season + "/" + day + "/" + year;
//         return rtn;
//     }
// }

// public class GameTime
// {
//     public int hour;
//     public int minute;
//     public GameTime()
//     {
//         hour = 0;
//         minute = 0;

//     }
//     public GameTime(int hour, int minute)
//     {
//         this.hour = hour;
//         this.minute = minute;
//     }
//     public void IncrementHour()
//     {
//         hour += 1;
//         if (hour > 23)
//         {
//             hour = 0;
//         }
//     }
// }

// public class TimeManager : MonoBehaviour
// {
//     public int minutes = 0;
//     public int hours = 0;
//     public string currDay = "Mon";
//     public string currSeason = "Spr";
//     public int year = 1;
//     public Date date;
//     public GameTime gameTime;

//     public float minutesPerSecond = 1.0f;

//     private string[] days = {"Mon", "Tue", "Wed", "Thr", "Fri", "Sat", "Sun"};
//     private float elapsedTime = 0.0f;
//     public int dayCounter = 0;

//     private string[] seasons = {"Spr", "Sum", "Fall", "Win"};
//     public int seasonCounter;

//     private bool isPaused;

//     void Update()
//     {
//         if (isPaused)
//         {
//             return;
//         }
//         elapsedTime += UnityEngine.Time.deltaTime;
//         if (elapsedTime >= minutesPerSecond)
//         {
//             elapsedTime -= minutesPerSecond;
//             UpdateTime();
//         }
//     }

//     void UpdateTime()
//     {
//         minutes += 1;
//         TimeEventHandler.TriggerMinuteChangedEvent();
//         if (minutes >= 60)
//         {
//             minutes = 0;
//             hours += 1;
//             TimeEventHandler.TriggerHourChangedEvent();
//             if (hours >= 23)
//             {
//                 hours = 0;
//                 dayCounter += 1;
//                 currDay = days[(dayCounter) % 7];
//                 UpdateDate();
//                 TimeEventHandler.TriggerDayChangedEvent();

//                 if (dayCounter > 2)
//                 {
//                     dayCounter = 0;
//                     seasonCounter += 1;
//                     currSeason = seasons[seasonCounter];
//                     TimeEventHandler.TriggerSeasonChangedEvent();

//                     if (seasonCounter > 4)
//                     {
//                         seasonCounter = 0;
//                         year += 1;
//                         TimeEventHandler.TriggerYearChangedEvent();
//                     }
//                 }
//             }
//         }
//     }

//     private void UpdateDate()
//     {
//         this.date = new Date(seasonCounter, dayCounter, year);
//     }

//     public void PauseTime()
//     {
//         isPaused = true;
//     }

//     public void ResumeTime()
//     {
//         isPaused = false;
//     }

// }

