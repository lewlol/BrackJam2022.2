using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : UnityEngine.MonoBehaviour
{
    public float health;
    public float maxHealth;

    [SerializeField] private GameObject fuel;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject nuggets;

    private PlanetShake pShake;

    public EnemyData[] redEnemies;
    public EnemyData[] greenEnemies;
    public EnemyData[] blueEnemies;
    public EnemyData[] yellowEnemies;

    public GameObject enemy;
    private Vector3 location;

    public GameObject vendor;

    //Sounds
    public AudioSource hitSource;
    public AudioSource explodeSource;
    private void Awake()
    {
        maxHealth = Random.Range(3, 15);
        health = maxHealth;
        pShake = GetComponent<PlanetShake>();

        SpawnEnemies();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        pShake.shakeDuration = 0.01f;
        hitSource.Play();
        StartCoroutine(ShakePlanet());  
        if (health <= 0)
        {
            explodeSource.Play();
            SpawnFuelCan();  
            SpawnNuggets();
            StartCoroutine(DestroyPlanet());   
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
    void SpawnNuggets()
    {
        int nugChange = Random.Range(0, 11);
        if(nugChange > 3)
        {
            int nugAmount = Random.Range(1, 4);
            for (int x = 0; x < nugAmount; x++)
            {
                Vector3 offset = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0);
                Vector3 spawnPos = gameObject.transform.position + offset;
                Instantiate(nuggets, spawnPos, Quaternion.identity);
            }
        }
    }

    void SpawnEnemies()
    {
        //Can Spawn Enemies?
        int spawnEnemy = Random.Range(0, 11);
        if(spawnEnemy > 1)
        {
            //Team?
            int team = Random.Range(0, 11);
            if(team < 2) //Red Team
            {
                int enemyCount = Random.Range(2, 11);
                for (int x = 0;x < enemyCount; x++)
                {
                    EnemyLocation();
                    var en = Instantiate(enemy, location, Quaternion.identity);
                    en.SetActive(false);

                    //Home Planet Assigning
                    en.GetComponent<EnemyAI>().homePlanet = gameObject;

                    int RandomEnemy = Random.Range(0, redEnemies.Length);
                    en.GetComponent<EnemyAI>().eData = redEnemies[RandomEnemy];
                    en.SetActive(true);
                    en.GetComponent<EnemyAI>().enabled = true;
                }
            }
            else if(team >= 2 && team < 4) //Blue Team
            {
                int enemyCount = Random.Range(2, 11);
                for (int x = 0; x < enemyCount; x++)
                {
                    EnemyLocation();
                    var en = Instantiate(enemy, location, Quaternion.identity);
                    en.SetActive(false);

                    //Home Planet Assigning
                    en.GetComponent<EnemyAI>().homePlanet = gameObject;

                    int RandomEnemy = Random.Range(0, blueEnemies.Length);
                    en.GetComponent<EnemyAI>().eData = blueEnemies[RandomEnemy];
                    en.SetActive(true);
                    en.GetComponent<EnemyAI>().enabled = true;
                }
            }
            else if(team >= 4 && team < 7) //Green Team
            {
                int enemyCount = Random.Range(2, 11);
                for (int x = 0; x < enemyCount; x++)
                {
                    EnemyLocation();
                    var en = Instantiate(enemy, location, Quaternion.identity);
                    en.SetActive(false);

                    //Home Planet Assigning
                    en.GetComponent<EnemyAI>().homePlanet = gameObject;

                    int RandomEnemy = Random.Range(0, greenEnemies.Length);
                    en.GetComponent<EnemyAI>().eData = greenEnemies[RandomEnemy];
                    en.SetActive(true);
                    en.GetComponent<EnemyAI>().enabled = true;
                }
            }
            else if(team >= 7) //Friendly Yellow Team with Vendor
            {
                int enemyCount = Random.Range(2, 11);
                for (int x = 0; x < enemyCount; x++)
                {
                    EnemyLocation();
                    var en = Instantiate(enemy, location, Quaternion.identity);
                    en.SetActive(false);

                    //Home Planet Assigning
                    en.GetComponent<EnemyAI>().homePlanet = gameObject;

                    int RandomEnemy = Random.Range(0, yellowEnemies.Length);
                    en.GetComponent<EnemyAI>().eData = yellowEnemies[RandomEnemy];
                    en.SetActive(true);
                    en.GetComponent<EnemyAI>().enabled = true;
                }

                //VendorSpawn
                Instantiate(vendor, gameObject.transform.position, Quaternion.identity);
                health = 99999;
            } 
        }
    }

    void EnemyLocation()
    {
        //SpawnLocation
        float xOffset;
        float yOffset;
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

        location = new Vector3(gameObject.transform.position.x + xOffset, gameObject.transform.position.y + yOffset, 0);
    }

    IEnumerator DestroyPlanet()
    {
        cam.GetComponent<CamShakePog>().enabled = true;

        CircleCollider2D cc = GetComponent<CircleCollider2D>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        cc.enabled = false;
        sr.enabled = false;

        var part = Instantiate(particles, gameObject.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        Destroy(part);
    }
    IEnumerator ShakePlanet()
    {
        pShake.enabled = true;
        yield return new WaitForSeconds(0.1f);
        pShake.enabled = false;
    }
}
