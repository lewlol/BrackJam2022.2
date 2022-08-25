using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : UnityEngine.MonoBehaviour
{
    public GameObject particles;
    public GameObject button;
    public int scene;
    public GameObject maincanvas;
    public GameObject infocanvas;
    public GameObject menu;
    public GameObject infomenu;
    public bool info;
    private void OnMouseEnter()
    {
        button.GetComponent<Spin>().enabled = true;
        button.transform.localScale = new Vector3(1.1f, 1.1f);
    }

    // Update is called once per frame
    private void OnMouseExit()
    {
        button.GetComponent<Spin>().enabled = false;
        button.transform.localScale = new Vector3(1.0f, 1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        info = true;
        maincanvas.SetActive(false);
        var p = Instantiate(particles, gameObject.transform.position, Quaternion.identity);
        button.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(p);
        infocanvas.SetActive(true);
        menu.SetActive(false);
        infomenu.SetActive(true);
        
    }

    private void Update()
    {
        if(info = true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
            

        

    }

}
