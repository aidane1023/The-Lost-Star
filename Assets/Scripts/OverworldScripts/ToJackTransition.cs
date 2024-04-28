using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToJackTransition : MonoBehaviour
{
    // Start is called before the first frame update
    OnTriggerEnter()
    {
        SceneManager.LoadScene ("JackTransition");
    }
}
