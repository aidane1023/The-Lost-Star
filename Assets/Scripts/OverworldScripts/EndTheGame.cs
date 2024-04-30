using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTheGame : MonoBehaviour
{
    void Start()
    {
      StartCoroutine(ToHub());  
    }

    void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("ThanksForPlayingScene");
        }
    }

    IEnumerator ToHub()
    {
        yield return new WaitForSeconds(26f);
        SceneManager.LoadScene ("ThanksForPlayingScene");
    }
}
