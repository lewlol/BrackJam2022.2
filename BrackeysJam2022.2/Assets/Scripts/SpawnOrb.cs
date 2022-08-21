using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOrb : MonoBehaviour
{
    public GameObject Orb;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Orb, gameObject.transform.position, Quaternion.identity);
        }
    }
}
