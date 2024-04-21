using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back2Hub : MonoBehaviour
{
    void OnTriggerEnter ()
    {
        SceneManager.LoadScene("HUBBuild");
    }
}
