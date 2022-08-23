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
    private void Awake()
    {
        maxHealth = Random.Range(3, 15);
        health = maxHealth;
        pShake = GetComponent<PlanetShake>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        pShake.shakeDuration = 0.01f;
        StartCoroutine(ShakePlanet());  
        if (health <= 0)
        {
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
