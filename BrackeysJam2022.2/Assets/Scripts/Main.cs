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

    //Fades
    public GameObject fadeIn;
    public GameObject fadeOut;



    private void Awake()
    {
        Time.timeScale = 1;
    }

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
        var p = Instantiate(particles, gameObject.transform.position, Quaternion.identity);
        button.GetComponent<SpriteRenderer>().enabled = false;
        GameObject startText = GameObject.Find("StartText");
        startText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        fadeIn.SetActive(true);
        fadeOut.SetActive(false);
        maincanvas.SetActive(false);
        Destroy(p);
        infocanvas.SetActive(true);
        menu.SetActive(false);
        infomenu.SetActive(true);
        
    }

    private void Update()
    {
        if(info = true && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ChangingToLevel());
        }
    }

    IEnumerator ChangingToLevel()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}
