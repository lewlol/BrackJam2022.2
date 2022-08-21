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
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
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
        }
    }
}
