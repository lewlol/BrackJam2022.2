using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float health;
    public float maxHealth;

    float amount = 1.0f; //how much it shakes

    [SerializeField] private GameObject fuel;

    [SerializeField]bool shaking;
    private void Awake()
    {
        maxHealth = Random.Range(3, 15);
        health = maxHealth;
    }
    private void FixedUpdate()
    {
        if (shaking)
        {
            Vector3 newPos = Random.insideUnitSphere * (Time.deltaTime * amount);

            transform.position = newPos;
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(Shaking());
        if (health <= 0)
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

    IEnumerator Shaking()
    {
        Vector3 ogPos = transform.position;

        if(shaking == false)
        {
            shaking = true;
        }

        yield return new WaitForSeconds(0.25f);

        shaking = false;
        transform.position = ogPos;
    }
}
