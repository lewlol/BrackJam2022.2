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
  
  
        

    private SpaceshipStats stats;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        stats = GetComponent<SpaceshipStats>();

    }

    // Update is called once per frame
    void Update()
    {
        yAxis = Input.GetAxis("Vertical");
        xAxis = Input.GetAxis("Horizontal");
        
        if(Input.GetKeyDown(KeyCode.Space) && stats.fuel > 10f)
        {
            Boost();
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
            stats.fuel -= Time.deltaTime;
        }
    }

    void Boost()
    {
        StartCoroutine(Boosting());
    }
    IEnumerator Boosting()
    {
        stats.fuel -= 5;
        yield return new WaitForSeconds(0.1f);
        extraspeed = 100;
        yield return new WaitForSeconds(0.2f);
        extraspeed = 15;
        

    }


  






}
