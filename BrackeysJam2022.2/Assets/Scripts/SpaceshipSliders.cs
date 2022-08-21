using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipSliders : MonoBehaviour
{
    public Slider health;
    public Slider fuel;

    [SerializeField] private SpaceshipStats stats;

    private void Start()
    {
        health.maxValue = stats.maxHealth;
        health.value = stats.health;

        fuel.maxValue = stats.maxFuel;
        fuel.value = stats.fuel;
    }

    private void Update()
    {
        health.maxValue = stats.maxHealth;
        fuel.maxValue = stats.maxFuel;


        health.value = stats.health;
        fuel.value = stats.fuel;
    }
}
