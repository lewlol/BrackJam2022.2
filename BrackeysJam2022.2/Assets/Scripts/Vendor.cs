using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    public Rigidbody2D player;
    public Canvas stats;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            player.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("clicked");
            stats.enabled = false;
        }
    }


}
