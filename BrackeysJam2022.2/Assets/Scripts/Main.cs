using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject button;
    public int scene;
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
        button.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

}
