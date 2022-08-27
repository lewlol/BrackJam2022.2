using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conspacemove : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;

    public float maxVelocity = 3;
    public float rotationSpeed = 3;
    public float extraspeed = 15;
    float yAxis;
    float xAxis;

    private TrailRenderer tr;

    private SpaceshipStats stats;

    bool hasFuel;

    public AudioSource boostSource;
    public AudioSource moveSource;
    public AudioClip move;
    public AudioClip booster;

    bool isBoosting;
    void Start()
    {
        tr = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();  
        stats = GetComponent<SpaceshipStats>();

    }

    // Update is called once per frame
    void Update()
    {
        if(stats.fuel > 0)
        {
            hasFuel = true;

        }else
        {
            hasFuel = false;
            extraspeed = 3f;
        }

        if (hasFuel)
        {
            //Detect Movement
            yAxis = Input.GetAxis("Vertical");
            xAxis = Input.GetAxis("Horizontal");

            //Trail Hiding Fixes
            HidingBoost();

            //Boost Detection
            if (Input.GetKeyDown(KeyCode.Space) && stats.fuel > 10f && yAxis > 0 && !isBoosting)
            {
                Boost();
                boostSource.clip = booster;
                boostSource.Play();
            }

            //Slow Speed Backwards
            if (yAxis < 0)
            {
                extraspeed = 10;
            }
            if (yAxis == 0)
            {
                extraspeed = 15;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            moveSource.Stop();
        }

        if (Input.GetKey(KeyCode.W) && moveSource.isPlaying == false && stats.fuel > 0)
        {
            moveSource.clip = move;
            moveSource.Play();
        }

        if (!hasFuel)
        {
            moveSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        ThrustForward(yAxis);
        Fueldeplete();
    }


    private void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount * extraspeed;
        rb.AddForce(force);
    }

    void Fueldeplete()
    {
       if(xAxis != 0 || yAxis != 0)
        {
            stats.fuel -= Time.deltaTime * 1.4f;
        }
    }

    void Boost()
    {
        StartCoroutine(Boosting());
    }
    IEnumerator Boosting()
    {
        isBoosting = true;
        stats.fuel -= 3;
        yield return new WaitForSeconds(0.1f);
        extraspeed = 100;
        yield return new WaitForSeconds(0.2f);
        extraspeed = 15;
        yield return new WaitForSeconds(0.3f);
        isBoosting=false;
    }

    void HidingBoost()
    {
        if (yAxis > 0 && tr.enabled == false)
        {
            tr.enabled = true;
        }
        if (yAxis < 0 && tr.enabled == true)
        {
            StartCoroutine(HideBoost());
        }
    }
    IEnumerator HideBoost()
    {
        yield return new WaitForSeconds(0.4f);
        tr.enabled = false;
    }
}
