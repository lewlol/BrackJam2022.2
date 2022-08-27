using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Retry : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject tryagain;
    public SpaceshipStats stats;


    private void Update()
    {
        if(stats.fuel <= 0)
        {
            tryagain.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.R) && tryagain.activeSelf == true)
        {
            StartCoroutine(Fade());
        }
        if(stats.fuel >= 1)
        {
            tryagain.SetActive(false);
        }
    }
    public void TryAgain()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("SampleScene");
    }
}
