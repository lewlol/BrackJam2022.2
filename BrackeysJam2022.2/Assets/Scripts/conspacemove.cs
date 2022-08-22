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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        ThrustForward(yAxis);
      
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


  






}
