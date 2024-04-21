using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{

    public GameObject menuStart, menuControls, menuCredits, menuExit, defaultMain, defaultCredits, defaultControls, creditsMenu;
    public GameObject[] credits;
    int currentMenu;
    private GameObject savedOption;
    private Button startButton, controlsButton, creditsButton, exitButton;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        startButton = menuStart.GetComponent<Button>();
        controlsButton = menuControls.GetComponent<Button>();
        creditsButton = menuCredits.GetComponent<Button>();
        exitButton = menuExit.GetComponent<Button>();

        currentMenu = 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuStart);

    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            switch (currentMenu)
            {
                case 0:
                EventSystem.current.SetSelectedGameObject(menuStart);
                break;

                case 1:
                EventSystem.current.SetSelectedGameObject(defaultCredits);
                break;

                default:
                break;
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (currentMenu == 1);
            {
                creditsMenu.SetActive(false);
                ReturnMenu(savedOption);
                Debug.Log(savedOption);
                currentMenu = 0;
            }
        }
    }

    public void ReturnMenu(GameObject returnOption)
    {
        if (currentMenu == 1)
        {
            startButton.interactable = true;
            controlsButton.interactable = true;
            creditsButton.interactable = true;
            exitButton.interactable = true;   
        }
        
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(returnOption);
    }

    public void OpenCredits()
    {
        currentMenu = 1;
        savedOption = menuCredits;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultCredits);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exit Successful");
    }
}
