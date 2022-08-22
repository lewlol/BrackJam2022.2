using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    //States
    bool friendlyState;
    bool neutralState;
    bool agressiveState;

    //Types
    bool neutralType; //Changes States - Normal Type of Alien
    bool friendlyType; //Never Attacks
    bool agressiveType; //Always Agressive State
    bool patrollingType; //Patrolling Randomly in Space

    private void Start()
    {
        neutralState = true;
    }

    private void FixedUpdate()
    {
        Movement();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        if (friendlyState) //Friendly State - Completely Ignores Player - Gain This for Heling out aliens
        {
            //Patrol Planet
        }
        else if (neutralState) //Neutral State - Docile Until the Player Attacks alien or Planet - Starting State
        {
            //Patrol Planet but closer to Player
        }
        else if (agressiveState) //Aggressive State - Attacking State for when the Player is Agressive
        {
            //Chase Player if in certain Distance
            //Shoot At Player
        }
    }
}
