using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderObjects : UnityEngine.MonoBehaviour
{
    public List <GameObject> objects;
    [SerializeField] private GameObject player;
    public Camera cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Planet" || collision.gameObject.tag == "Asteroid")
        {
            collision.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Planet" || collision.gameObject.tag == "Asteroid")
        {
            collision.gameObject.SetActive(false);
        }
    }
    void RefreshObjects()
    {
        foreach(var obj in objects)
        {
            if(obj == null)
            {
                objects.Remove(obj);
                return;
            }
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
