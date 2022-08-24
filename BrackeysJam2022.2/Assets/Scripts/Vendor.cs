using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    public Rigidbody2D player;
    public Canvas stats;
    public Canvas vendorui; 

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            player.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("clicked");
            stats.enabled = false;
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
