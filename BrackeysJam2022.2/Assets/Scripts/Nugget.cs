using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nugget : MonoBehaviour
{

    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(pickupnugget());
            other.GetComponent<SpaceshipStats>().nuggets++;
        }
    }


    IEnumerator pickupnugget()
    {
        //particle for picking up
        //sound for picking up
        Destroy(this.gameObject);
        yield return new WaitForSeconds(1);
        
    }
}
