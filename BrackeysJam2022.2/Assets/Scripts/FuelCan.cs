using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCan : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<SpaceshipStats>().fuel += 3f;
            Destroy(gameObject);
        }
    }
}
