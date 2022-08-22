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

        int ran = Random.Range(0, 2);
        if (ran == 1)
        {
            SpawnFuelCan();
        }

        //Scale Change
        float scale = (health / maxHealth);
        transform.localScale = new Vector3(scale, scale, scale);
    }
    void SpawnFuelCan()
    {
        Quaternion ranRot = new Quaternion(0, 0, Random.Range(0, 180), 0);
        var fuelCan = Instantiate(fuel, gameObject.transform.position, ranRot);
        fuel.GetComponent<Rigidbody2D>().AddForce(transform.forward * 20f);
    }

}
