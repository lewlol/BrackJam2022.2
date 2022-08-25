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

    [Header("Hit Particles")]
    public GameObject particles;

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
                if(collision.gameObject.GetComponent<EnemyAI>() != null)
                {
                    collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
                    SpawnParticles();
                }
                Destroy(gameObject);
                return;
            }
            if(collision.gameObject.tag == "Planet")
            {
                if(collision.gameObject.GetComponent<Planet>() != null)
                {
                    collision.gameObject.GetComponent<Planet>().TakeDamage(damage);
                    SpawnParticles();
                    Destroy(gameObject);
                    return;
                }
            }
        }
        if (passThrough)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
                SpawnParticles();
            }
            if (collision.gameObject.tag == "Planet")
            {
                collision.gameObject.GetComponent<Planet>().TakeDamage(damage);
                SpawnParticles();
            }
        }
    }

    void SpawnParticles()
    {
        Instantiate(particles, gameObject.transform.position, Quaternion.identity);
    }

}
