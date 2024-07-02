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
