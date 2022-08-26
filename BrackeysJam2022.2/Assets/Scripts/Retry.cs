using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Retry : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject tryagain;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && tryagain.activeSelf == true)
        {
            StartCoroutine(Fade());
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
