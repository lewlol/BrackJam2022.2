using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float health;
    public float maxHealth;

    float speed = 1.0f; //how fast it shakes
    float amount = 1.0f; //how much it shakes

    [SerializeField] private GameObject fuel;

    [SerializeField]bool shaking;
    Vector2 startPos;
    private void Awake()
    {
        maxHealth = Random.Range(3, 15);
        health = maxHealth;
    }
    private void FixedUpdate()
    {
        float x = gameObject.transform.position.x * Mathf.Sin(Time.time * speed) * amount;
        float y = gameObject.transform.position.y * Mathf.Sin(Time.time * speed) * amount;
        float z = gameObject.transform.position.z;

        gameObject.transform.position = new Vector3(x, y, z);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            SpawnFuelCan();
            Destroy(gameObject);
        }  
    }
    void SpawnFuelCan()
    {
        int fueldrop = Random.Range(1, 4);
        for(int x = 0; x < fueldrop; x++)
        {
            Vector3 offset = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0);
            Vector3 spawnPos = gameObject.transform.position + offset;
            Instantiate(fuel, spawnPos, Quaternion.identity);
        }
    }
}
