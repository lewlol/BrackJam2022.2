using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //eData Reference - Where the Stats will be Assigned For
    public EnemyData eData;

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
    private Vector2 hPlanet;

    //Team - Which team the Enemy is On
    bool redTeam;
    bool greenTeam;
    bool blueTeam;
    bool yellowTeam;

    //Friendly State - Never Attacks Player 
    bool friendlyState;
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

    //Death Particles
    GameObject dParticles;

    //Player/Enemy Reference - To Track and Kill Player/Enemy
    public GameObject player;
    float playerDistance;

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

    //Dead Bool
    bool dead;

    //Cam Reference
    GameObject cam;

    //Kamikaze KB
    float strength = 25;
    float kbDelay = 0.15f;

    //Detect Planet Hit
    float planetOGhealth;

    //Enemy Drops
    [SerializeField] private GameObject fuelDrop;

    //Sounds
    public AudioClip kamiSound;
    public AudioSource shootingSource;
    public AudioSource deathSource;
    public AudioSource hitSound;

    private void Awake()
    {
        //Assignables
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        //Assign Stats from eData
        AssignStats();

        //Find a Location
        findingLocation = true;

        //Saving the Planets Location (Stops Error on Destroying the Planet)
        hPlanet = homePlanet.transform.position;

        //Dead Check
        dead = false;

        cam = GameObject.FindGameObjectWithTag("MainCamera");

        planetOGhealth = homePlanet.GetComponent<Planet>().health;
    }
    private void FixedUpdate()
    {
        if (!dead)
        {
            Movement();
        }
    }
    void AssignStats() //This Assigns Stats from the eData (Enemy Data Scriptable Object)
    {
        health = eData.health;
        speed = eData.speed;
        chaseRadius = eData.chaseRadius;
        attackRadius = eData.attackRadius;
        damage = eData.damage;
        dParticles = eData.dParticles;

        sprite = eData.sprite;
        sr.sprite = sprite;

        redTeam = eData.redTeam;
        greenTeam = eData.greenTeam;
        blueTeam = eData.blueTeam;
        yellowTeam = eData.yellowTeam;

        friendlyState = eData.friendlyState;
        neutralState = eData.neutralState;
        agressiveState = eData.agressiveState;

        shooterType = eData.shooterType;
        kamikazeType = eData.kamikazeType;

        bulletSpeed = eData.bulletSpeed;
        bulletDelay = eData.bulletDelay;
        bulletLifetime = eData.bulletLifetime;
        bullet = eData.bullet;

        delay = eData.bulletDelay;

        //FindPlayer
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Movement() //Enemy Movement
    {
        //Distance Between Enemy and Player
        playerDistance = Vector2.Distance(transform.position, player.transform.position);

        //Despawn
        if(playerDistance >= 50)
        {
            Destroy(gameObject);
        }
        //Friendly State
        if (friendlyState)
        {
            Wandering();
        }

        //Neutral State
        if (neutralState)
        {
            Wandering();

            if(planetOGhealth > homePlanet.GetComponent<Planet>().health)
            {
                //Planet Hit
                PlanetAttacked();
            }
        }

        //Agressive State
        if (agressiveState)
        {

            //Cant Track Player
            if (playerDistance > chaseRadius)
            {
                Wandering();
            }

            //If Within Chase Radius
            if (playerDistance <= chaseRadius && !exitChase)
            {
                //Rotate to Player
                Vector3 targetPosition = player.transform.position;
                Vector3 dir = targetPosition - this.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.AngleAxis(angle + 270, Vector3.forward);

                float finalspeed = speed * Time.deltaTime; //This Moves to Position
                Vector2 position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position, finalspeed);
                rb.MovePosition(position);
            }

            //If Withing Shooting Distance
            if (playerDistance < attackRadius && !kamikazeType)
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
            //Create a Var
            var bul = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);

            //Assign Stats to Bullet
            bul.GetComponent<eBullet>().lifetime = bulletLifetime;
            bul.GetComponent<eBullet>().damage = damage;
            bul.GetComponent<eBullet>().passThrough = bulletPassThrough;
            bul.GetComponent<eBullet>().speed = bulletSpeed;

            Vector2 direction = transform.position - player.transform.position;
            bul.GetComponent<eBullet>().direction = direction;

            bul.GetComponent<eBullet>().StatsAssigned();

            shootingSource.Play();

            //Reset Timer
            delay = bulletDelay;
        }
    }

    void Wandering()
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

            location = new Vector3(hPlanet.x + xOffset, hPlanet.y + yOffset, 0);

            wandering = true;
            findingLocation = false;
        }

        if (wandering)
        {
            //Wander to Location
            Vector3 targetPosition = location; //Rotation
            Vector3 dir = targetPosition - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle + 270, Vector3.forward);

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
        hitSound.Play();

        if(health <= 0)
        {
            dead = true;
            deathSource.Play();
            StartCoroutine(Death());
        }

        if (neutralState)
        {
            neutralState = false;
            agressiveState = true;

            wandering = false;
            findingLocation = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (kamikazeType)
            {
                collision.gameObject.GetComponent<SpaceshipStats>().TakeDamage(damage);
                StartCoroutine(Death());
                StartCoroutine(CamShake());

                deathSource.clip = kamiSound;
                deathSource.Play();

                Vector2 direction = (transform.position + collision.transform.position).normalized;
                player.GetComponent<Rigidbody2D>().AddForce(direction * strength, ForceMode2D.Impulse);
            }
        }

        if (collision.gameObject.tag == "Planet")
        {
            StartCoroutine(WaitToMove());
        }
    }

    IEnumerator CamShake()
    {
        cam.GetComponent<CamShakePog>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        cam.GetComponent<CamShakePog>().enabled = false;
    }

    IEnumerator Death()
    {
        int fDrop = Random.Range(0, 2);
        if(fDrop == 0)
        {
            Instantiate(fuelDrop, gameObject.transform.position, Quaternion.identity);
        }

        CircleCollider2D cc = GetComponent<CircleCollider2D>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        cc.enabled = false;
        sr.enabled = false;

        Instantiate(dParticles, gameObject.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        Destroy(dParticles);
        Destroy(gameObject);
    }
    IEnumerator WaitToMove()
    {
        rb.freezeRotation = true;
        wandering = false;
        waitingToMove = true;

        float delay = Random.Range(0, 8);
        yield return new WaitForSeconds(delay);

        waitingToMove = false;

        location = Vector2.zero;
        
        findingLocation = true;
        rb.freezeRotation = false;
    }
}
