using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrb : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private float maxMoveSpeed = 10f;
    private float smoothTime = 0.3f;
    private float minDistance = 2f;
    private Vector2 currentVelocity;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }
    private void Update()
    {
        MoveOrb();
    }
    void MoveOrb()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.SmoothDamp(transform.position, mousePosition, ref currentVelocity, smoothTime, maxMoveSpeed);
    }
}
