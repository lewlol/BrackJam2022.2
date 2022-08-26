using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetindicator : MonoBehaviour
{
    public Transform Target;
    public GameObject arrow;

    private void Update()
    {
        var dir = Target.position - transform.position;

        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

}
