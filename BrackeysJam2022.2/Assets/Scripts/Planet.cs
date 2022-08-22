using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float health;
    public float maxHealth;

    [SerializeField] private GameObject fuel;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            SpawnFuelCan();
            Destroy(gameObject);
        }

        //Scale Change
        float scale = (health / maxHealth);
        if(scale < 0.4f)
        {
            scale = 0.4f;
        }
        transform.localScale = new Vector3(scale, scale, scale);
    }
    void SpawnFuelCan()
    {
        Instantiate(fuel, gameObject.transform.position, Quaternion.identity);
    }

}
