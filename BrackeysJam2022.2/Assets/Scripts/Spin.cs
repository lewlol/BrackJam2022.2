using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 0.5f;
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, -speed));
    }
}
