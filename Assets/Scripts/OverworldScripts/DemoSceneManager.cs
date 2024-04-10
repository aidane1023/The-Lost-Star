using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoSceneManager : MonoBehaviour
{
    public static bool home;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            home = true;
            SceneManager.LoadScene("HUBBuild");
        }
    }
}
