using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextNugget : MonoBehaviour
{
    public Text nuggetamount;
    public GameObject player;


    // Update is called once per frame
    void Update()
    {
        nuggetamount.text = "Nuggets: " + player.GetComponent<SpaceshipStats>().nuggets.ToString();
    }
}
