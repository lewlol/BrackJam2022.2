using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipStats : MonoBehaviour
{
    public float health;
    [HideInInspector]public float maxHealth;

    public float fuel;
    [HideInInspector]public float maxFuel;

    private void Start()
    {
        health = 100;
        maxHealth = 100;

        fuel = 100;
        maxFuel = 100;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
