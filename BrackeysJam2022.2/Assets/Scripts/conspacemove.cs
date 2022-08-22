using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conspacemove : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;

    public float maxVelocity = 3;
    public float rotationSpeed = 3;
    public float extraspeed = 1;
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
    }

    private void FixedUpdate()
    {
        ThrustForward(yAxis);
        Fueldeplete();
    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x, y);
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


  






}
