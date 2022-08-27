using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : UnityEngine.MonoBehaviour
{
    [SerializeField] private GameObject activeBullet;
    public float bSpeed = 7f;
    private float bulletLife = 2f;
    public GameObject player;

    public AudioSource shootSource;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && player.GetComponent<SpaceshipStats>().fuel > 1)
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
        shootSource.Play();
    }
}
