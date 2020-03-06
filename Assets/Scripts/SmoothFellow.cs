using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFellow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.130f;
    public Vector3 offset;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(target);
    }
}
