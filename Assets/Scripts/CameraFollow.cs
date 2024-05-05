using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    Vector3 camOffset;

    void Start()
    {
        camOffset = transform.position - target.position;
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + camOffset, Time.deltaTime * 5f);
    }
}
