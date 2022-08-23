using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : UnityEngine.MonoBehaviour
{
    float x;
    float y;

    public GameObject player;
    private void Start()
    {
        //Decide Positive or Negative X
        int posNegX = Random.Range(0, 2);
        if(posNegX == 0)
        {
            x = Random.Range(250, 450);
        }
        else
        {
            x = Random.Range(-250, -450);
        }

        //Decide Positive or Negative Y
        int posNegY = Random.Range(0, 2);
        if(posNegY == 0)
        {
            y = Random.Range(250, 450);
        }
        else
        {
            y = Random.Range(-250, -450);
        }

        player.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
