using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData eData;

    //Enemy Stats
    [Header("Stats")]
    public float health;
    public float speed;
    public float extraspeed;

    //Types
    [Header("Enemy Type")]
    [SerializeField] bool neutralType; //Changes States - Normal Type of Alien
    [SerializeField] bool friendlyType; //Never Attacks
    [SerializeField] bool agressiveType; //Always Agressive State
    [SerializeField] bool patrollingType; //Patrolling Randomly in Space
    [SerializeField] bool kamikazeType; //Flys into the Player Exploding

    //Weapon Stats
    [Header("Weapon Stats")]
    public float bulspeed;
    public float bulDelay;

    //States
    [Header("State")]
    [SerializeField] bool friendlyState;
    [SerializeField] bool neutralState;
    [SerializeField] bool agressiveState;

    //Other Assignables
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject parentPlanet;
    [SerializeField] private GameObject target;

    //Random Vars
    public float countdown;
    float delay = 0.15f;
    float attackRadius = 5f;
    Vector2 movePos;
    bool foundLocation;
    Rigidbody2D rb;
    private void Start()
    {
        neutralState = true;
        target = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        AssignStats();
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
            if (foundLocation) //Find a New Location
            {
                    Vector2 planetPos = parentPlanet.transform.position;
                    float xOffset = Random.Range(-15, 15);
                    float yOffset = Random.Range(-15, 15);
                    movePos = new Vector2(planetPos.x + xOffset, planetPos.y + yOffset);
                    rb.constraints = RigidbodyConstraints2D.None;

                    foundLocation = false;
            }
            if (!foundLocation) //Travel to New Location
            {          
                var distance = Vector3.Distance(transform.position, movePos);
                if (distance > 3f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, movePos, speed * Time.deltaTime);
                }else
                {
                    StartCoroutine(FindNewLocation());
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }
        }
        else if (neutralState) //Neutral State - Docile Until the Player Attacks alien or Planet - Starting State
        {
            //Patrol Planet but closer to Player
        }
        else if (agressiveState) //Aggressive State - Attacking State for when the Player is Agressive
        {
            //Look At Player
            transform.rotation = Quaternion.FromToRotation(transform.position, -target.transform.position);

            //Distance Calc and Shooting Timer
            countdown = delay -= Time.deltaTime;
            var distance = Vector3.Distance(transform.position, target.transform.position);

            //Charge At Player
            if(distance > attackRadius)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }

            //Stop and Shoot
            if (distance <= attackRadius)
            {
                if (countdown <= 0)
                {
                    GameObject bul = Instantiate(bullet, transform.position, transform.rotation);
                    Rigidbody2D bulRB = bul.GetComponent<Rigidbody2D>();
                    Vector2 direction = transform.position - target.transform.position;
                    bulRB.AddForce(-direction * bulspeed, ForceMode2D.Impulse);
                    bullet.GetComponent<eBullet>().lifetime = 1.5f;

                    delay = 0.5f;
                }
            }
        }
    }


    IEnumerator FindNewLocation()
    {
        int count = Random.Range(0, 11);
        Debug.Log(count);
        yield return new WaitForSeconds(count);
        foundLocation = true;
    }

    void AssignStats()
    {
        //Enemy Stats
        health = eData.health;
        speed = eData.speed;
        extraspeed = eData.eSpeed;

        //Enemy Type
        friendlyType = eData.friendly;
        neutralType = eData.neutral;
        agressiveType = eData.agressive;

    }
}
