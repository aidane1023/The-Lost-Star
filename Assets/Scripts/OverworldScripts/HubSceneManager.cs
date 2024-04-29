using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubSceneManager : MonoBehaviour
{
    public int scene;
    public static Vector3 leftHub;

    public Transform player;

    PopUp popupScript;

    void Awake()
    {
        popupScript = GetComponent<PopUp>();
    }

    void Update()
    {
        if (TrainingDummy.cleared && (popupScript == null || popupScript.interactable))
        {
            if ((Vector3.Distance(player.position, this.transform.position) <= 3f) && Input.GetKeyUp(KeyCode.Z))
            {
                switch (scene)
                {
                case 0:
                    leftHub = player.position;
                    SceneManager.LoadScene ("BearShop");
                    break;
                case 1:
                    leftHub = player.position;
                    SceneManager.LoadScene ("LevelOne");
                    break;
                case 2:
                        leftHub = player.position;
                        SceneManager.LoadScene("LevelTwo");
                        break;
                case 3:
                        leftHub = player.position;
                        SceneManager.LoadScene("LevelThree");
                        break;
                case 4:
                    //WinGame?
                    break;
                }   
            }
        }
    }
}

