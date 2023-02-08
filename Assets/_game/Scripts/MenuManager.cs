using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    private void Start()
    {
        // menu = GameObject.Find("Menu Panel");

    }
    public void ShowMenu()
    {
        menu.SetActive(true);
    }
    public void HideMenu()
    {
        menu.SetActive(false);

    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
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
