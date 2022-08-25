using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipStats : UnityEngine.MonoBehaviour
{
    public float health;
    [HideInInspector]public float maxHealth;

    public float fuel;
    [HideInInspector]public float maxFuel;

    public float nuggets;

    [SerializeField] private GameObject particles;

    private void Start()
    {
        health = 100;
        maxHealth = 100;

        fuel = 100;
        maxFuel = 100;

        nuggets = 0;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            StartCoroutine(Death());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BlackHole")
        {
            StartCoroutine(Death());
        }
    }
    private void FixedUpdate()
    {
        if(fuel > maxFuel)
        {
            fuel = maxFuel;
        }
        if(fuel < 0)
        {
            fuel = 0;
        }
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    IEnumerator Death()
    {
        CircleCollider2D cc = GetComponent<CircleCollider2D>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        cc.enabled = false;
        sr.enabled = false;

        var part = Instantiate(particles, gameObject.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(3f);

        Destroy(part);
        Destroy(gameObject);
    }
}
