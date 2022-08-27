using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public AudioSource pickupSource;
    public GameObject buffText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Remove());
            collision.gameObject.GetComponent<SpaceshipStats>().health += 7f;

            Vector3 offset = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
            var bText = Instantiate(buffText, transform.position + offset, Quaternion.identity);
            TextMesh tm = bText.GetComponent<TextMesh>();
            tm.text = "+10 HP";
            tm.color = Color.red;
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
