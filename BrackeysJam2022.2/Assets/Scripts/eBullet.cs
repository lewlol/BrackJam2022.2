using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eBullet : UnityEngine.MonoBehaviour
{
    [Header("Variables")]
    public float lifetime;
    public float damage;
    public float speed;

    [HideInInspector] public Vector2 direction;
    [Header("Bools")]
    public bool passThrough;

    Rigidbody2D bulRB;

    private void Awake()
    {   
        bulRB = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        direction.Normalize();
        bulRB.AddForce(-direction * speed, ForceMode2D.Impulse);
    }

    public void StatsAssigned()
    {
        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!passThrough)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<SpaceshipStats>().TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
            if (collision.gameObject.tag == "Planet")
            {
                collision.gameObject.GetComponent<Planet>().TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
        }
        if (passThrough)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<SpaceshipStats>().TakeDamage(damage);
            }
            if (collision.gameObject.tag == "Planet")
            {
                collision.gameObject.GetComponent<Planet>().TakeDamage(damage);
            }
        }
    }
}
