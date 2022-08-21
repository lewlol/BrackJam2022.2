using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime;
    public bool passThrough;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!passThrough)
        {
            if (gameObject.tag == "Bullet")
            {
                if (collision.gameObject.tag == "Enemy")
                {
                    Destroy(gameObject);
                }
            }
            if (gameObject.tag == "Enemy")
            {
                if (collision.gameObject.tag == "Player")
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
