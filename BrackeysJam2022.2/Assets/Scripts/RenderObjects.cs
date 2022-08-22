using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderObjects : MonoBehaviour
{
    public GameObject[] objects;
    [SerializeField] private GameObject player;
    public Camera cam;

    private void FixedUpdate()
    {
        RefreshObjects();
    }
    void RefreshObjects()
    {
        foreach(var obj in objects)
        {
            float distance = Vector2.Distance(player.transform.position, obj.transform.position);
            if(distance <= cam.orthographicSize * 5)
            {
                obj.SetActive(true);
            }else
            {
                obj.SetActive(false);
            }
        }
    }
}
