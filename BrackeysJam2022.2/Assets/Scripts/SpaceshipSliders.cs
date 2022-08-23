using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipSliders : UnityEngine.MonoBehaviour
{
    public Slider health;
    public Slider fuel;
    public Text nugget;

    [SerializeField] private SpaceshipStats stats;

    private void Start()
    {
        health.maxValue = stats.maxHealth;
        health.value = stats.health;

        fuel.maxValue = stats.maxFuel;
        fuel.value = stats.fuel;

        nugget.text = "         " + stats.nuggets.ToString();
    }

    private void Update()
    {
        health.maxValue = stats.maxHealth;
        fuel.maxValue = stats.maxFuel;


        health.value = stats.health;
        fuel.value = stats.fuel;

        nugget.text = "         " + stats.nuggets.ToString();
    }
}
