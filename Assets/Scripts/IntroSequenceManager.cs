using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSequenceManager : MonoBehaviour
{
    void Start()
    {
      StartCoroutine(ToHub());  
    }

    IEnumerator ToHub()
    {
        yield return new WaitForSeconds(21f);
        SceneManager.LoadScene ("HUBBuild");
    }
}
