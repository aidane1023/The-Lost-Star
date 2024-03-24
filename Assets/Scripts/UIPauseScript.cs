using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIPauseScript : MonoBehaviour
{
    public GameObject pauseMenu, pausePage, pauseFirstButton;

    bool isPauseActive;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        
    }

    // Update is called once per frame
    void Update()
    {
        isPauseActive = pauseMenu.activeInHierarchy;
        if (Input.GetButtonDown("Cancel"))
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }
        if (isPauseActive)
        {
           Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ExitMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
