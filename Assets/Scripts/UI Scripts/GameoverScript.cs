using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameoverScript : MonoBehaviour
{

    public GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(continueButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null) EventSystem.current.SetSelectedGameObject(continueButton);
    }

    public void Continue()
    {
        BattleManager.overworldSpawn = new Vector3(-10,0,-9);
        PlayerBattler.health = PlayerBattler.maxHealth;
        PlayerBattler.starPoints = PlayerBattler.maxStarPoints;
        SceneManager.LoadScene("HUBBuild");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
}
