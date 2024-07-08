// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraFollow : MonoBehaviour
// {
//     [SerializeField] private Transform target;
//     private BoxCollider2D bounds; // Reference to the BoxCollider2D
//     Vector3 camOffset;

//     private float camHalfHeight;
//     private float camHalfWidth;

//     void Start()
//     {
//         bounds = GetComponent<BoxCollider2D>();
//         transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
//         camOffset = transform.position - target.position;

//         // Calculate camera half size based on orthographic size and aspect ratio
//         camHalfHeight = Camera.main.orthographicSize;
//         camHalfWidth = Camera.main.aspect * camHalfHeight;
//     }

//     void LateUpdate() // Use LateUpdate for camera movement to ensure it happens after other updates
//     {
//         Vector3 targetPosition = target.position + camOffset;
//         Vector3 clampedPosition = ClampCamera(targetPosition);

//         transform.position = Vector3.Lerp(transform.position, clampedPosition, Time.deltaTime * 5f);
//     }

//     private Vector3 ClampCamera(Vector3 targetPosition)
//     {
//         Bounds boxBounds = bounds.bounds;

//         // Clamp camera position to box bounds
//         float minX = boxBounds.min.x + camHalfWidth;
//         float maxX = boxBounds.max.x - camHalfWidth;
//         float minY = boxBounds.min.y + camHalfHeight;
//         float maxY = boxBounds.max.y - camHalfHeight;

//         float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
//         float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);

//         return new Vector3(clampedX, clampedY, targetPosition.z);
//     }
// }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    Vector3 camOffset;

    void Start()
    {
        // transform.position = target.position;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        camOffset = transform.position - target.position;
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + camOffset, UnityEngine.Time.deltaTime * 5f);
    }
}
