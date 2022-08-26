using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nugget : UnityEngine.MonoBehaviour
{
    [SerializeField] private GameObject nugParticles;
    public AudioSource pickupSource;
    public GameObject buffText; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            pickupSource.Play();
            StartCoroutine(pickupnugget());
            other.GetComponent<SpaceshipStats>().nuggets++;

            Vector3 offset = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
            var bText = Instantiate(buffText, transform.position + offset, Quaternion.identity);
            TextMesh tm = bText.GetComponent<TextMesh>();
            tm.text = "+1 Nugget";
            tm.color = Color.yellow;
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
