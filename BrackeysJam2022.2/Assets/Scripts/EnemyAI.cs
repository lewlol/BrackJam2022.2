using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //eData Reference - Where the Stats will be Assigned For
    [SerializeField] private EnemyData eData;

    //Stats - The Enemies Stats
    float health;
    float speed;
    float chaseRadius;
    float attackRadius;
    float damage;

    //Sprite Renderer and Sprite
    Sprite sprite;
    SpriteRenderer sr;

    //Home Planet Reference - Wander Around this Point
    public GameObject homePlanet;

    //Team - Which team the Enemy is On
    bool redTeam;
    bool greenTeam;
    bool blueTeam;

    //Neutral State - Doesn't Attack until the Player Attacks them or Their Planet
    bool neutralState;
    //Aggressive State - Tracks the Player in a Certain Radius and Attacks
    bool agressiveState;

    //Shooter Type - Shoots the Player
    bool shooterType;
    //Kamikaze Type - Drives into the Player and Explodes
    bool kamikazeType;

    //Bullet Speed
    float bulletSpeed;
    //Bullet Delay
    float bulletDelay;
    //Bullet Lifetime
    float bulletLifetime;
    //Bullet Pass Through
    bool bulletPassThrough;
    //Bullet Prefab
    GameObject bullet;

    //Player/Enemy Reference - To Track and Kill Player/Enemy
    public GameObject player;

    //Rigidbody
    Rigidbody2D rb;

    //Wandering Bools
    bool findingLocation;
    bool wandering;
    bool waitingToMove;

    //Wandering Location
    float xOffset;
    float yOffset;
    Vector3 location;
    float distance;

    //Chasing Bool
    bool exitChase;

    //Shooting Vars
    float delay;

    private void Awake()
    {
        //Assignables
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        //Assign Stats from eData
        AssignStats();

        //Find a Location
        findingLocation = true;
    }
    private void FixedUpdate()
    {
        Movement();
    }
    void AssignStats() //This Assigns Stats from the eData (Enemy Data Scriptable Object)
    {
        health = eData.health;
        speed = eData.speed;
        chaseRadius = eData.chaseRadius;
        attackRadius = eData.attackRadius;
        damage = eData.damage;

        sprite = eData.sprite;
        sr.sprite = sprite;

        redTeam = eData.redTeam;
        greenTeam = eData.greenTeam;
        blueTeam = eData.blueTeam;

        neutralState = eData.neutralState;
        agressiveState = eData.agressiveState;

        shooterType = eData.shooterType;
        kamikazeType = eData.kamikazeType;

        bulletSpeed = eData.bulletSpeed;
        bulletDelay = eData.bulletDelay;
        bulletLifetime = eData.bulletLifetime;
        bullet = eData.bullet;

        delay = eData.bulletDelay;
    }

    void Movement() //Enemy Movement
    {
        if (neutralState) //Neutral State - Wander Around Home Planet
        {       
            if (findingLocation)
            {
                //Find a New Location to Wander to
                float xposNeg = Random.Range(0, 2);
                if (xposNeg == 0)
                {
                    xOffset = Random.Range(7, 12);
                }
                else
                {
                    xOffset = Random.Range(-7, -12);
                }
                float yposNeg = Random.Range(0, 2);
                if (yposNeg == 0)
                {
                    yOffset = Random.Range(7, 12);
                }
                else
                {
                    yOffset = Random.Range(-7, -12);
                }

                location = new Vector3(homePlanet.transform.position.x + xOffset, homePlanet.transform.position.y + yOffset, 0);

                wandering = true;
                findingLocation = false;           
            }

            if (wandering)
            {
                //Wander to Location
                Vector3 dir = location - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                float finalspeed = speed * Time.deltaTime; //This Moves to Position
                Vector2 position = Vector2.MoveTowards(gameObject.transform.position, location, finalspeed);
                rb.MovePosition(position);

                //Once in Location Wait a Random amount of Time
                distance = Vector2.Distance(gameObject.transform.position, position);
                if (distance == 0)//Arrived at Location
                {
                    if (!waitingToMove)
                    {
                        StartCoroutine(WaitToMove());
                    }
                }
            }
        }

        if (agressiveState)
        {
            //Distance Between Enemy and Player
            float playerDistance = Vector2.Distance(transform.position, player.transform.position);

            //If Within Chase Radius
            if (playerDistance <= chaseRadius && !exitChase)
            {
                float finalspeed = speed * Time.deltaTime; //This Moves to Position
                Vector2 position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position, finalspeed);
                rb.MovePosition(position);
            }

            //If Withing Shooting Distance
            if (playerDistance < attackRadius)
            {
                Shooting();
            }
        }
    }
    void Shooting()
    {
        float countdown = delay -= Time.deltaTime;
        if (countdown <= 0)
        {
            //Instantiate Bullet
            var bul = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);

            //Assign Stats to Bullet
            bul.GetComponent<eBullet>().lifetime = bulletLifetime;
            bul.GetComponent<eBullet>().damage = damage;
            bul.GetComponent<eBullet>().passThrough = bulletPassThrough;

            //Bullet Momentum
            Rigidbody2D rb = bul.GetComponent<Rigidbody2D>();
            rb.AddForce(bul.transform.up * bulletSpeed, ForceMode2D.Impulse);

            //Reset Timer
            delay = bulletDelay;
        }
    }
    public void PlanetAttacked()
    {
        neutralState = false;
        agressiveState = true;

        wandering = false;
        findingLocation = false;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;

        neutralState = false;
        agressiveState = true;

        wandering = false;
        findingLocation = false;
    }
    IEnumerator WaitToMove()
    {
        wandering = false;
        waitingToMove = true;

        float delay = Random.Range(0, 8);
        Debug.Log(delay);
        yield return new WaitForSeconds(delay);

        waitingToMove = false;

        location = Vector2.zero;
        
        findingLocation = true;
    }
}
