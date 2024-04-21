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

    public GameObject menuStart, menuControls, menuCredits, menuExit, defaultMain, defaultCredits, defaultControls;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuStart);
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(menuStart);
        }
    }

    public void StartGame()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exit Successful");
    }
}
