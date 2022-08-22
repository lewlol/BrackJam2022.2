using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Variables")]
    public float lifetime;
    public float damage;
    [Header("Bools")]
    public bool passThrough;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!passThrough)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if(collision.gameObject.GetComponent<Enemy>() != null)
                {
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                }
                Destroy(gameObject);
                return;
            }
            if(collision.gameObject.tag == "Planet")
            {
                collision.gameObject.GetComponent<Planet>().TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
        }
        if (passThrough)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
            if (collision.gameObject.tag == "Planet")
            {
                collision.gameObject.GetComponent<Planet>().TakeDamage(damage);
            }
        }
    }
}
