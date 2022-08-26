using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public AudioSource pickupSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Remove());
            collision.gameObject.GetComponent<SpaceshipStats>().health += 10f;
        }
    }

    IEnumerator Remove()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        BoxCollider2D bx = gameObject.GetComponent<BoxCollider2D>();

        sr.enabled = false;
        bx.enabled = false;

        pickupSource.Play();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
