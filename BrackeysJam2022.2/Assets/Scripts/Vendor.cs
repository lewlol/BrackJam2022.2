using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Vendor : MonoBehaviour
{
    //Players Rigidbody
    public Rigidbody2D player;

    //Main Canvas
    public GameObject canvas;

    //Health, Fuel, Nuggets, Distance
    public GameObject healthFuel;
    public GameObject distance;
    public GameObject nuggets;

    //Vendor UI
    public GameObject vendorUI;

    //Alien Names
    public string[] firstname;
    public string[] lastname;
    //Planet Names
    public string[] planetname;
    //Alien Avatars
    public Sprite[] aliens;
    //Dialogue Options
    public string[] dialogue;

    //name
    public Text alienname;
    //planet description text
    public Text planetdescription;
    //dialogue text
    public Text dialoguetxt;
    //Alien Avatar
    public Image alien;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        healthFuel = GameObject.Find("Bars");
        distance = GameObject.Find("EarthDistance");
        nuggets = GameObject.Find("NuggetCount");
        vendorUI = GameObject.Find("VendorUI");


        alienname = GameObject.Find("Name").GetComponent<Text>();
        planetdescription = GameObject.Find("AlienPlanetName").GetComponent<Text>();
        dialoguetxt = GameObject.Find("AlienDesc").GetComponent<Text>();
        alien = GameObject.Find("AVimage").GetComponent<Image>();

        int fName = Random.Range(0, firstname.Length);
        int lName = Random.Range(0, lastname.Length);
        int pname = Random.Range(0, planetname.Length);
        int dial = Random.Range(0, dialogue.Length);
        int alienA = Random.Range(0, aliens.Length);

        alien.sprite = aliens[alienA];
        alienname.text = firstname[fName] + " " + lastname[lName];
        planetdescription.text = "Welcome to  " + planetname[pname];
        dialoguetxt.text = dialogue[dial];
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            player.constraints = RigidbodyConstraints2D.FreezeAll;
            
            healthFuel.SetActive(false);
            distance.SetActive(false);
            nuggets.SetActive(false);

            vendorUI.SetActive(true);
        }

        if(other.gameObject.tag == "Player"&& Input.GetKeyDown(KeyCode.Escape))
        {
            player.constraints = RigidbodyConstraints2D.None;

            healthFuel.SetActive(true);
            distance.SetActive(true);
            nuggets.SetActive(true);

            vendorUI.SetActive(false);
        }
    }


}
