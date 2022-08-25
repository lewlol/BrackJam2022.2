using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : UnityEngine.MonoBehaviour
{
    public GameObject Player;
    public Rigidbody2D prigidbody;
    public Canvas ui;
    public GameObject earth;
    public Camera maincam;

    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        ui.enabled = false;
        prigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        maincam.GetComponent<CamFollow>().target = earth.transform;
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(1);
    }

}
