using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class NPCMovement : MonoBehaviour
// {
//     // private Dictionary<int, List<Path>> weeklySchedule = new Dictionary<int, List<Path>>();
//     public ScheduleData currDailySchedule;
//     public float movementSpeed = 2f;
//     private int currentCheckpointIndex = 0;
//     public bool isMoving;

//     private Animator anim;
//     private NPCData npcData;
//     private Transform npcTransform;

//     // Start is called before the first frame update
//     void Start()
//     {
//         anim = GetComponent<Animator>();
//         npcData = GetComponent<NPCCharacter>().npcData;
//         npcTransform = GetComponent<Transform>();
//         currDailySchedule = npcData.weeklySchedule[GameManager.instance.timeManager.date.day];
//         PredictLocation();
//         // StartCoroutine(MoveToNextPoint());
//         TimeEventHandler.OnHourChanged += StartHourActivity;
//         StartHourActivity();
//     }

//     private void StartHourActivity()
//     {
//         if (currDailySchedule.dailySchedule[GameManager.instance.timeManager.gameTime.hour] is IdleData)
//         {
//             IdleData idle = (IdleData) currDailySchedule.dailySchedule[GameManager.instance.timeManager.gameTime.hour];
//             transform.position = idle.position;
//             isMoving = false;
//             anim.SetBool("isMoving", false);
//             anim.SetFloat("Horizontal", idle.direction.x);
//             anim.SetFloat("Vertical", idle.direction.y);
//         }
//         else
//         {
//             PathData path = (PathData) currDailySchedule.dailySchedule[GameManager.instance.timeManager.gameTime.hour];
//             StartCoroutine(StartPath(path));
//         }
//     }

//     private IEnumerator StartPath(PathData path)
//     {
//         foreach (Checkpoint pathpoint in path.pathpoints)
//         {
//             yield return StartCoroutine(MoveToNextPoint(pathpoint.position));
//         }
//     }

//     private IEnumerator MoveToNextPoint(Vector3 newPosition)
//     {
//         Vector3 currPosition = this.gameObject.transform.position;
//         Vector3 difference = currPosition - newPosition;
//         while (currPosition != newPosition)
//         {
//             currPosition = this.gameObject.transform.position;
//             difference = currPosition - newPosition;
//             if (difference.x != 0)
//             {
//                 if (difference.x > 0)
//                 {
//                     yield return MoveToPosition(new Vector3(currPosition.x - 1, currPosition.y, currPosition.z));
//                 }
//                 else
//                 {
//                     yield return MoveToPosition(new Vector3(currPosition.x + 1, currPosition.y, currPosition.z));
//                 }
//             }
//             if (difference.y != 0)
//             {
//                 if (difference.y > 0)
//                 {
//                     yield return MoveToPosition(new Vector3(currPosition.x, currPosition.y - 1, currPosition.z));
//                 }
//                 else
//                 {
//                     yield return MoveToPosition(new Vector3(currPosition.x, currPosition.y + 1, currPosition.z));
//                 }
//             }
//         }
//         yield return null;

//     }

//     private IEnumerator MoveToPosition(Vector3 targetPosition)
//     {
//         isMoving = true;
//         anim.SetBool("isMoving", true);
//         Vector3 difference = (targetPosition - transform.position).normalized;
//         anim.SetFloat("Horizontal", difference.x);
//         anim.SetFloat("Vertical", difference.y);
//         while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
//         {
//             transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
//             yield return null;
//         }
//     }

//     public void PredictLocation()
//     {
//         int day = GameManager.instance.timeManager.date.day;
//         GameTime time = GameManager.instance.timeManager.gameTime;
//         currentCheckpointIndex = 0;

//         ///moving if pathdata
//         if (npcData.weeklySchedule[day].dailySchedule[time.hour] is IdleData)
//         {
//             IdleData currHourIdle = (IdleData) npcData.weeklySchedule[day].dailySchedule[time.hour];
//         }
//         else if (npcData.weeklySchedule[day].dailySchedule[time.hour] is PathData)
//         {
//             PathData currHourSchedule = (PathData) npcData.weeklySchedule[day].dailySchedule[time.hour];
//             float elapsedTimeMinutes = time.minute;
//             Vector3 finalPosition = currHourSchedule.pathpoints[0].position; // Start from the first point

//             for (int i = 0; i < currHourSchedule.pathpoints.Count - 1; i++)
//             {
//                 Vector3 startPoint = currHourSchedule.pathpoints[i].position;
//                 Vector3 endPoint = currHourSchedule.pathpoints[i + 1].position;

//                 float distance = Vector3.Distance(startPoint, endPoint);
//                 float timeToTravel = distance / movementSpeed;

//                 if (elapsedTimeMinutes <= timeToTravel)
//                 {
//                     float t = elapsedTimeMinutes / timeToTravel;
//                     finalPosition = Vector3.Lerp(startPoint, endPoint, t);
//                     break;
//                 }
//                 else
//                 {
//                     Debug.Log("Incrementing current checkpoint index");
//                     elapsedTimeMinutes -= timeToTravel;
//                     currentCheckpointIndex++;
//                 }
//             }
//             npcTransform.position = finalPosition;
//         }
//     }

//     private void OnDestroy()
//     {
//         TimeEventHandler.OnHourChanged -= StartHourActivity;
//     }
// }


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
        // StartCoroutine(MoveToNextCheckpoint());
        StartActivity();
        TimeEventHandler.OnHourChanged += StartActivity;
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
            currentCheckpointIndex = 0;
            npcTransform.position = ((PathData) currDailySchedule.dailySchedule[GameManager.instance.timeManager.gameTime.hour]).pathpoints[0].position;
            StartCoroutine(MoveToNextCheckpoint());
        }
    }

    private IEnumerator MoveToNextCheckpoint()
    {
        PathData path = (PathData) currDailySchedule.dailySchedule[GameManager.instance.timeManager.gameTime.hour];

        Debug.Log(path.pathpoints.Count);
        for (int i = currentCheckpointIndex; i < path.pathpoints.Count; i++)
        {
            Vector3 pathpoint = path.pathpoints[i].position;

            Debug.Log("Moving to this position: " +  pathpoint);
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
