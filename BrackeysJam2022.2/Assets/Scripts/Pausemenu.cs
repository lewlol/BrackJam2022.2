using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;
    [SerializeField] private GameObject othercanvas;


    private void Update()

    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }


        if(Input.GetKeyDown(KeyCode.R) && isPaused )
        {
            DeactivateMenu();
        }

        if (Input.GetKeyDown(KeyCode.M) && isPaused)
        {
            DeactivateMenu();
            MainMenu();
        }

        if (Input.GetKeyDown(KeyCode.Q) && isPaused)
        {
            Exit();
        }


    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        AudioListener.pause = true;
        othercanvas.SetActive(false);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        AudioListener.pause = false;
        isPaused = false;
        othercanvas.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("quited");
    }





}