using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Stats")]
    public float health; //Enemy Health
    public float speed; //Enemy Speed
    public float eSpeed; //Enemy eSpeed
    public Sprite sprite; //Enemy Sprite

    [Header("Team")]
    public bool redTeam; //Red Team
    public bool greenTeam; //Green Team
    public bool blueTeam; //Blue Team

    [Header("Enemy Type")]
    public bool shooterType; //Shoots Player
    public bool kamikazeType; //Flys into Player (Cant Shoot)

    [Header("Default State")]
    public bool neutralState; //Wont Hurt Player Immediately
    public bool agressiveState; //Will Hurt Player Immediately

    [Header("Weapon Stats")]
    public float bulletSpeed; //Bullet Speed
    public float bulletDelay; //Bullet Delay
}
