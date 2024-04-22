using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back2Hub : MonoBehaviour
{
    void OnTriggerEnter ()
    {
        SceneManager.LoadScene("HUBBuild");
    }
}
