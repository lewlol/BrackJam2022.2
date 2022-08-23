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
    float eSpeed;

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
        eSpeed = eData.eSpeed;

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
                Vector3 diff = (location - transform.position); //This Aquires Rotation
                float angle = Mathf.Atan2(diff.x, diff.y);
                transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);

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

            //Check for Player Damaging Planet or Enemy
            //Switch to Agressive State
        }
    }

    void Shooting()
    {

    }
    public void TakeDamage(float damage)
    {
        health -= damage;

        neutralState = false;
        agressiveState = true;
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
