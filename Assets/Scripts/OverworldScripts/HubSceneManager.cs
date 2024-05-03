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
        if (TrainingDummy.cleared && (popupScript == null || popupScript.interactable) && !HubManager.allCleared)
        {
            if ((Vector3.Distance(player.position, this.transform.position) <= 3f) && Input.GetButtonUp("Submit"))
            {
                switch (scene)
                {
                case 0:
                    HubManager.hubSpawn = player.position;
                    SceneManager.LoadScene ("BearShop");
                    HubManager.fromShop = false;
                    break;
                case 1:
                    HubManager.hubSpawn = player.position;
                    SceneManager.LoadScene ("LevelOne");
                    HubManager.fromShop = false;
                    break;
                case 2:
                    HubManager.hubSpawn = player.position;
                    SceneManager.LoadScene("LevelTwo");
                    HubManager.fromShop = false;
                    break;
                case 3:
                    HubManager.hubSpawn = player.position;
                    SceneManager.LoadScene("LevelThree");
                    HubManager.fromShop = false;
                    break;
                }   
            }
        }
    }
}

