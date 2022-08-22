using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody2D prigidbody;
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
        prigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }

}
