using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Chest : MonoBehaviour
{
    [SerializeField] GameObject chestClose;
    [SerializeField] GameObject chestOpen;
    [SerializeField] GameObject menuManager;

    private void Start()
    {
        menuManager = GameObject.Find("Game Manager");
        chestClose.SetActive(true);
        chestOpen.SetActive(false);
        menuManager.GetComponent<MenuManager>().HideMenu();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "container")
        {
            menuManager.GetComponent<MenuManager>().ShowMenu(); // khi container va cham voi chest thi ShowMenu
            chestClose.SetActive(false);
            chestOpen.SetActive(true);
            collision.collider.gameObject.GetComponent<Container>().onBridge = false; 
            collision.collider.gameObject.GetComponent<Container>().enabled = false; // tat script cua container
        }
    }
}
