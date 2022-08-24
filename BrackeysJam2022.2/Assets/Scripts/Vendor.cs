using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Vendor : MonoBehaviour
{
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
    //Background
    public GameObject bg;
    //Alien BG
    public GameObject alienBG;


    private void Awake()
    {
        healthFuel = GameObject.Find("Bars");
        distance = GameObject.Find("EarthDistance");
        nuggets = GameObject.Find("NuggetCount");
        vendorUI = GameObject.Find("VendorUI");


        alienname = GameObject.Find("AlienName").GetComponent<Text>();
        planetdescription = GameObject.Find("AlienPlanetName").GetComponent<Text>();
        dialoguetxt = GameObject.Find("AlienDesc").GetComponent<Text>();
        alien = GameObject.Find("AVimage").GetComponent<Image>();
        bg = GameObject.Find("BG");
        alienBG = GameObject.Find("Avatar");

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            OpenMenu();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CloseMenu();
        }
    }

    void OpenMenu()
    {

        healthFuel.SetActive(false);
        distance.SetActive(false);
        nuggets.SetActive(false);

        vendorUI.SetActive(true);
        alienname.enabled = true;
        alien.enabled = true;
        alienBG.GetComponent<Image>().enabled = true;
        planetdescription.enabled = true;
        dialoguetxt.enabled = true;
        bg.GetComponent<Image>().enabled = true;
    }

    void CloseMenu()
    {

        healthFuel.SetActive(true);
        distance.SetActive(true);
        nuggets.SetActive(true);

        vendorUI.SetActive(false);

        vendorUI.SetActive(false);
        alienname.enabled = false;
        alien.enabled = false;
        alienBG.GetComponent<Image>().enabled = false;
        planetdescription.enabled = false;
        dialoguetxt.enabled = false;
        bg.GetComponent<Image>().enabled = false;
    }

}
