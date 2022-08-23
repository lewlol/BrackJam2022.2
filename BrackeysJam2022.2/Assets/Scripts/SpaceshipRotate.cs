using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipRotate : UnityEngine.MonoBehaviour
{
    [SerializeField] Camera cam;
    private void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;

        Vector3 objectPos = cam.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
