using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vendor : MonoBehaviour
{
    public Rigidbody2D player;
    public Canvas stats;
    public Canvas vendorui;
    public string[] firstname;
    public string[] lastname;
    public Text alienname;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            player.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("clicked");
            stats.enabled = false;

            int fName = Random.Range(0, firstname.Length);
            int lName = Random.Range(0, lastname.Length);

            alienname.text = firstname[fName] + " " + lastname[lName];
            vendorui.enabled = true;
        }

        if(other.gameObject.tag == "Player"&& Input.GetKeyDown(KeyCode.Escape))
        {
            player.constraints = RigidbodyConstraints2D.None;
            stats.enabled = true;
            vendorui.enabled = false;
        }
    }


}
