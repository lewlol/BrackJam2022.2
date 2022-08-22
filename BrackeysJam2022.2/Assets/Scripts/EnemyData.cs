using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Stats")]
    public float health;
    public float speed;
    public float eSpeed;
    public Sprite sprite;

    [Header("Enemy Type")]
    public bool friendly;
    public bool agressive;
    public bool neutral;
    public bool patrolling;
    public bool kamikaze;

    [Header("Weapon Stats")]
    public float bulletSpeed;
    public float bulletDelay;
}
