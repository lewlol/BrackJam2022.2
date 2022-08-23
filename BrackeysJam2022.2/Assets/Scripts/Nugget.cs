using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nugget : UnityEngine.MonoBehaviour
{
    [SerializeField] private GameObject nugParticles;
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
        var nPart = Instantiate(nugParticles, gameObject.transform.position, Quaternion.identity);

        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        bc.enabled = false;
        sr.enabled = false;

        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        Destroy(nPart);
    }
}
