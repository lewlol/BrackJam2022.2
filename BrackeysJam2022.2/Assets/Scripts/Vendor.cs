using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Vendor : MonoBehaviour
{
    //Player Reference
    public GameObject player;
    public GameObject bullet;

    //Main Canvas
    public GameObject canvas;

    //Health, Fuel, Nuggets, Distance
    public GameObject healthFuel;
    public GameObject distance;
    public GameObject nuggets;

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

    //Upgrades
    public Upgrades[] upgrade;
    private Upgrades activeUpgrade;
    //Upgrade Offer Text
    public Text upgradetext;
    public Text acceptText;

    bool boughtUpgrade;
    bool purchaseBool;

    public AudioClip deny;
    public AudioClip purchase;
    public AudioSource audio;
    public AudioSource tradertheme;

    public AudioSource music;
    float maxVolume = 0.2f;

    [SerializeField] private GameObject buffText;

    private void Awake()
    {
        music = GameObject.Find("music").GetComponent<AudioSource>();
        audio = GetComponentInChildren<AudioSource>();
        healthFuel = GameObject.Find("Bars");
        distance = GameObject.Find("EarthDistance");
        nuggets = GameObject.Find("NuggetCount");
        player = GameObject.Find("Spaceship");


        alienname = GameObject.Find("AlienName").GetComponent<Text>();
        planetdescription = GameObject.Find("AlienPlanetName").GetComponent<Text>();
        dialoguetxt = GameObject.Find("AlienDesc").GetComponent<Text>();
        alien = GameObject.Find("AVimage").GetComponent<Image>();
        bg = GameObject.Find("BG");
        alienBG = GameObject.Find("Avatar");
        upgradetext = GameObject.Find("UpgradeText").GetComponent<Text>();
        acceptText = GameObject.Find("AcceptText").GetComponent<Text>();

        int fName = Random.Range(0, firstname.Length);
        int lName = Random.Range(0, lastname.Length);
        int pname = Random.Range(0, planetname.Length);
        int dial = Random.Range(0, dialogue.Length);
        int alienA = Random.Range(0, aliens.Length);

        alien.sprite = aliens[alienA];
        alienname.text = firstname[fName] + "  " + lastname[lName];
        planetdescription.text = "Welcome to  " + planetname[pname];
        dialoguetxt.text = dialogue[dial];


        //Run Upgrade
        int randomUpgrade = Random.Range(0, upgrade.Length);
        activeUpgrade = upgrade[randomUpgrade];
        if (upgrade[randomUpgrade].upgradeInt == 0)
        {
            //Damage Upgrade
            upgradetext.text = "I Can Upgrade Your " + upgrade[randomUpgrade].name + " For " + upgrade[randomUpgrade].cost + " Nuggets";
        }
        else if (upgrade[randomUpgrade].upgradeInt == 1)
        {
            //HP Repair
            upgradetext.text = "I Can Repair Your " + upgrade[randomUpgrade].name + " For " + upgrade[randomUpgrade].cost + " Nuggets";
        }
        else if (upgrade[randomUpgrade].upgradeInt == 2)
        {
            //MaxHP Upgrade
            upgradetext.text = "I Can Upgrade Your " + upgrade[randomUpgrade].name + " For " + upgrade[randomUpgrade].cost + " Nuggets";
        }
    }
    private void Update()
    {
        if (purchaseBool && tradertheme.volume < maxVolume)
        {
            tradertheme.volume += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Y) && !boughtUpgrade && purchaseBool)
        {
            if (player.GetComponent<SpaceshipStats>().nuggets < activeUpgrade.cost)
            {
                audio.clip = deny;
                audio.Play();
                StartCoroutine(Poor());
            }
            else
            {
                audio.clip = purchase;
                audio.Play();
                upgradetext.text = "Thank You For Purchasing";
                acceptText.text = null;

                Vector3 offset = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
                var bText = Instantiate(buffText, player.transform.position + offset, Quaternion.identity);
                TextMesh tm = bText.GetComponent<TextMesh>();

                if (activeUpgrade.upgradeInt == 0)
                {
                    //Add Damage
                    bullet.GetComponent<Bullet>().damage += activeUpgrade.value;       
                    tm.text = "+" + activeUpgrade.value + " Damage";
                }
                else if (activeUpgrade.upgradeInt == 1)
                {
                    //Repair
                    player.GetComponent<SpaceshipStats>().health = player.GetComponent<SpaceshipStats>().maxHealth;
                    tm.text = "Health Restored!";
                }
                else if (activeUpgrade.upgradeInt == 2)
                {
                    //Max HP Increase
                    player.GetComponent<SpaceshipStats>().maxHealth += activeUpgrade.value;
                    player.GetComponent<SpaceshipStats>().health += activeUpgrade.value;

                    tm.text = "+" + activeUpgrade.value + " MaxHP";
                }
                player.GetComponent<SpaceshipStats>().nuggets -= activeUpgrade.cost;
            }
        }

        //Distance Between Enemy and Player
        float playerDistance = Vector2.Distance(transform.position, player.transform.position);

        //Despawn
        if (playerDistance >= 100)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            music.Pause();
            tradertheme.Play();
            purchaseBool = true;
            OpenMenu();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tradertheme.volume = 0f;
            music.Play();
            tradertheme.Stop();
            purchaseBool = false;
            CloseMenu();
        }
    }

    void OpenMenu()
    {

        healthFuel.SetActive(false);
        distance.SetActive(false);
        nuggets.SetActive(false);

        alienname.enabled = true;
        alien.enabled = true;
        alienBG.GetComponent<Image>().enabled = true;
        planetdescription.enabled = true;
        dialoguetxt.enabled = true;
        bg.GetComponent<Image>().enabled = true;
        acceptText.enabled = true;
        upgradetext.enabled = true;
    }

    void CloseMenu()
    {

        healthFuel.SetActive(true);
        distance.SetActive(true);
        nuggets.SetActive(true);

        alienname.enabled = false;
        alien.enabled = false;
        alienBG.GetComponent<Image>().enabled = false;
        planetdescription.enabled = false;
        dialoguetxt.enabled = false;
        bg.GetComponent<Image>().enabled = false;
        acceptText.enabled = false;
        upgradetext.enabled = false;
    }
    IEnumerator Poor()
    {
        string beforeUpgrade = upgradetext.text;
        string accText = acceptText.text;

        upgradetext.text = "You Don't Have Enough Nuggets";
        acceptText.text = null;

        yield return new WaitForSeconds(3f);

        upgradetext.text = beforeUpgrade;
        acceptText.text = accText;
    }
}
