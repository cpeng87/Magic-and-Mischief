using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCMovement : MonoBehaviour
{
    public ScheduleData currDailySchedule;
    public float movementSpeed;
    public bool isMoving;

    private Animator anim;
    private NPCData npcData;
    private Transform npcTransform;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        npcData = GetComponent<NPCCharacter>().npcData;
        movementSpeed = npcData.movementSpeed;
        npcTransform = GetComponent<Transform>();
        currDailySchedule = npcData.weeklySchedule[GameManager.instance.timeManager.date.day];
        StartActivity();
        TimeEventHandler.OnHourChanged += StartActivity;
        TimeEventHandler.OnDayChanged += UpdateDailySchedule;
    }

    private void UpdateDailySchedule()
    {
        currDailySchedule = npcData.weeklySchedule[GameManager.instance.timeManager.date.day];
    }

    private void StartActivity()
    {
        if (currDailySchedule.dailySchedule[GameManager.instance.timeManager.gameTime.hour] is IdleData)
        {
            IdleData idle = (IdleData) currDailySchedule.dailySchedule[GameManager.instance.timeManager.gameTime.hour];
            transform.position = idle.position;
            isMoving = false;
            anim.SetBool("isMoving", false);
            anim.SetFloat("Horizontal", idle.direction.x);
            anim.SetFloat("Vertical", idle.direction.y);
        }
        else if (currDailySchedule.dailySchedule[GameManager.instance.timeManager.gameTime.hour] is PathData)
        {
            int index = PredictLocation();
            PathData path = (PathData) currDailySchedule.dailySchedule[GameManager.instance.timeManager.gameTime.hour];

            //already at the end of path
            if (index >= path.pathpoints.Count - 1)
            {
                npcTransform.position = path.pathpoints[path.pathpoints.Count - 1].position;
                isMoving = false;
                anim.SetBool("isMoving", false);
                anim.SetFloat("Horizontal", path.endDirection.x);
                anim.SetFloat("Vertical", path.endDirection.y);
            }
            else
            {
                // npcTransform.position = path.pathpoints[index].position;
                StartCoroutine(MoveToNextCheckpoint(index));
            }
        }
    }

    private IEnumerator MoveToNextCheckpoint(int currentCheckpointIndex)
    {
        PathData path = (PathData) currDailySchedule.dailySchedule[GameManager.instance.timeManager.gameTime.hour];

        for (int i = currentCheckpointIndex; i < path.pathpoints.Count; i++)
        {
            Vector3 pathpoint = path.pathpoints[i].position;
            if (path.pathpoints[i].isSwapScene)
            {
                //check if has swapped or not
                if (SceneManager.GetActiveScene().name != path.pathpoints[i].mapName)
                {
                    GameManager.instance.npcManager.UpdateLocation(npcData, path.pathpoints[currentCheckpointIndex].newMap);
                    Destroy(this.gameObject);
                    yield return null;
                }
                else
                {
                    npcTransform.position = path.pathpoints[currentCheckpointIndex].position;
                }
            }
            yield return StartCoroutine(MoveToPosition(pathpoint));
        }
        
        isMoving = false;
        anim.SetBool("isMoving", false);
        anim.SetFloat("Horizontal", path.endDirection.x);
        anim.SetFloat("Vertical", path.endDirection.y);
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        isMoving = true;
        anim.SetBool("isMoving", true);
        Vector3 difference = (targetPosition - transform.position).normalized;
        anim.SetFloat("Horizontal", difference.x);
        anim.SetFloat("Vertical", difference.y);
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public int PredictLocation()
    {
        int currentCheckpointIndex = 0;
        int day = GameManager.instance.timeManager.date.day;
        GameTime time = GameManager.instance.timeManager.gameTime;
        currentCheckpointIndex = 0;

        if (npcData.weeklySchedule[day].dailySchedule[time.hour] is PathData)
        {
            PathData currHourSchedule = (PathData) npcData.weeklySchedule[day].dailySchedule[time.hour];
            float elapsedTimeMinutes = time.minute;
            Vector3 finalPosition = currHourSchedule.pathpoints[0].position; // Start from the first point

            for (int i = 0; i < currHourSchedule.pathpoints.Count - 1; i++)
            {
                Vector3 startPoint = currHourSchedule.pathpoints[i].position;
                Vector3 endPoint = currHourSchedule.pathpoints[i + 1].position;

                float distance = Vector3.Distance(startPoint, endPoint);
                float timeToTravel = distance / movementSpeed;
                float minutesForTravel = GameManager.instance.timeManager.ConvertRealTimeToMinutes(timeToTravel);
                
                if (elapsedTimeMinutes <= minutesForTravel)
                {
                    float t = elapsedTimeMinutes / minutesForTravel;
                    finalPosition = Vector3.Lerp(startPoint, endPoint, t);
                    break;
                }
                else
                {
                    elapsedTimeMinutes -= minutesForTravel;
                    currentCheckpointIndex++;
                }
            }
            npcTransform.position = finalPosition;
            return currentCheckpointIndex + 1;
        }
        return 0;
    }

    private void OnDestroy()
    {
        TimeEventHandler.OnHourChanged -= StartActivity;
        TimeEventHandler.OnDayChanged -= UpdateDailySchedule;
    }
}
