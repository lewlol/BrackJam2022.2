using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : UnityEngine.MonoBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject player;
    private void Start()
    {
        int spawn = Random.Range(0, spawnLocations.Length);
        player.transform.position = new Vector3(spawnLocations[spawn].transform.position.x, spawnLocations[spawn].transform.position.y, -3f);
    }
}
