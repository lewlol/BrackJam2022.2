using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOrb : MonoBehaviour
{
    public GameObject Orb;
    [SerializeField] private float orbCount;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(orbCount >= 1)
            {
                Instantiate(Orb, gameObject.transform.position, Quaternion.identity);
                orbCount--;
            }     
        }
    }
}
