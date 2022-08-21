using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject activeBullet;
    private float bSpeed = 5f;
    private float bulletLife = 2f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject bul = Instantiate(activeBullet, transform.position, transform.rotation);
        Rigidbody2D bulRB = bul.GetComponent<Rigidbody2D>();
        bulRB.AddForce(bul.transform.up * bSpeed, ForceMode2D.Impulse);
        activeBullet.GetComponent<Bullet>().lifetime = bulletLife;
    }
}
