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
        // Debug.Log("Starting move to next checkpoint");
        PathData path = currDailySchedule.dailySchedule[GameManager.instance.timeManager.gameTime.hour];

        for (int i = currentCheckpointIndex + 1; i < path.pathpoints.Count; i++)
        {
            Vector3 pathpoint = path.pathpoints[i].position;
            yield return StartCoroutine(MoveToPosition(pathpoint));
        }
        // yield return StartCoroutine(MoveToPosition(path.pathpoints[path.pathpoints.Count - 1].position));
        
        isMoving = false;
        anim.SetBool("isMoving", false);
        // yield return new WaitForSeconds(path.pathpoints[path.pathpoints.Count - 1].stayTime);

        currentCheckpointIndex++;
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

        // Get the current hour schedule
        PathData currHourSchedule = npcData.weeklySchedule[day].dailySchedule[time.hour];
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
                elapsedTimeMinutes -= timeToTravel;
                currentCheckpointIndex++;
            }
        }
        npcTransform.position = finalPosition;
    }

    private void OnDestroy()
    {
        TimeEventHandler.OnHourChanged -= StartActivity;
    }
}
