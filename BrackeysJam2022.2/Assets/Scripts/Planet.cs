using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public void TakeDamage(float damage)
    {
        health -= damage;

        //Scale Change
        float scale = (health / maxHealth);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
