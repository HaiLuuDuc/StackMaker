using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject losePanel;

    [SerializeField] GameObject settingsButton;




    private void Start()
    {
        // menu = GameObject.Find("Menu Panel");
        HidePause();
        HideMenu();
        HideLose();
    }
    public void ShowMenu()
    {
        settingsButton.SetActive(false);
        menu.SetActive(true);
    }
    public void HideMenu()
    {
        menu.SetActive(false);
    }
    public void ShowLose()
    {
        settingsButton.SetActive(false);
        losePanel.SetActive(true);
    }
    public void HideLose()
    {
        losePanel.SetActive(false);
    }

    public void Play1()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void Play2()
    {
        SceneManager.LoadScene("Scene2");
    }
    public void Play3()
    {
        SceneManager.LoadScene("Scene3");
    }


    public void ShowPause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void HidePause()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoHome()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
