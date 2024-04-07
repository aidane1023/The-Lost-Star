using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubSceneManager : MonoBehaviour
{
    public int scene;

    public Transform player;

    void Update()
    {
        if (TrainingDummy.cleared)
        {
            if ((Vector3.Distance(player.position, this.transform.position) <= 3f) && Input.GetKeyUp(KeyCode.Z))
            {
                switch (scene)
                {
                case 0:
                    SceneManager.LoadScene ("Shop");
                    break;
                case 1:
                    SceneManager.LoadScene ("LevelOne");
                    break;
                case 2:
                    //LevelTwo
                    break;
                case 3:
                    //LevelThree
                    break;
                case 4:
                    //WinGame?
                    break;
                }   
            }
        }

        if (Input.GetKeyUp(KeyCode.H))
        {
            SceneManager.LoadScene ("HUBBuild");
        }
    }
}

