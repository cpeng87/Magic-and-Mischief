using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCMovement : MonoBehaviour
{
    // private Dictionary<int, List<Path>> weeklySchedule = new Dictionary<int, List<Path>>();
    public ScheduleData currDailySchedule;
    public float movementSpeed = 2f;
    private int currentCheckpointIndex = 0;
    public bool isMoving;

    private Animator anim;
    private NPCData npcData;
    private Transform npcTransform;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        npcData = GetComponent<NPCCharacter>().npcData;
        npcTransform = GetComponent<Transform>();
        currDailySchedule = npcData.weeklySchedule[GameManager.instance.timeManager.date.day];
        PredictLocation();
        StartCoroutine(MoveToNextCheckpoint());
        TimeEventHandler.OnHourChanged += StartActivity;
    }

    private void StartActivity()
    {
        StartCoroutine(MoveToNextCheckpoint());
    }

    private IEnumerator MoveToNextCheckpoint()
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
            PathData path = (PathData) currDailySchedule.dailySchedule[GameManager.instance.timeManager.gameTime.hour];

            Debug.Log("Start printing");
            foreach (Checkpoint checkpoint in path.pathpoints)
            {
                Debug.Log(checkpoint.position);
            }
            Debug.Log("End printing");

            for (int i = currentCheckpointIndex; i < path.pathpoints.Count; i++)
            {
                Vector3 pathpoint = path.pathpoints[i].position;
                if (pathpoint == new Vector3(0,0,0))
                {
                    Debug.Log("Index of (0,0,0)" + currentCheckpointIndex);
                }

                Debug.Log("Moving to this position: " +  pathpoint);
                yield return StartCoroutine(MoveToPosition(pathpoint));
            }
            // yield return StartCoroutine(MoveToPosition(path.pathpoints[path.pathpoints.Count - 1].position));
            
            isMoving = false;
            anim.SetBool("isMoving", false);
            // yield return new WaitForSeconds(path.pathpoints[path.pathpoints.Count - 1].stayTime);

            currentCheckpointIndex++;
        }
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

    public void PredictLocation()
    {
        int day = GameManager.instance.timeManager.date.day;
        GameTime time = GameManager.instance.timeManager.gameTime;
        currentCheckpointIndex = 0;

        ///moving if pathdata
        if (npcData.weeklySchedule[day].dailySchedule[time.hour] is IdleData)
        {
            IdleData currHourIdle = (IdleData) npcData.weeklySchedule[day].dailySchedule[time.hour];
        }
        else if (npcData.weeklySchedule[day].dailySchedule[time.hour] is PathData)
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

                if (elapsedTimeMinutes <= timeToTravel)
                {
                    float t = elapsedTimeMinutes / timeToTravel;
                    finalPosition = Vector3.Lerp(startPoint, endPoint, t);
                    break;
                }
                else
                {
                    Debug.Log("Incrementing current checkpoint index");
                    elapsedTimeMinutes -= timeToTravel;
                    currentCheckpointIndex++;
                }
            }
            npcTransform.position = finalPosition;
        }
    }

    private void OnDestroy()
    {
        TimeEventHandler.OnHourChanged -= StartActivity;
    }
}
