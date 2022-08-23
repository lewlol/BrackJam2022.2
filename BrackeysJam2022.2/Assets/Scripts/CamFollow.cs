using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public float dampTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    private Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }
    private void LateUpdate()
    {
            if (target)
            {
                Vector3 point = cam.WorldToViewportPoint(target.position);
                Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                Vector3 destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }
    }
}
