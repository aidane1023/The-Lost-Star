using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTheEnd : MonoBehaviour
{
    void Start()
    {
      StartCoroutine(ToEnd());  
    }

    IEnumerator ToEnd()
    {
        yield return new WaitForSeconds(26f);
        SceneManager.LoadScene ("ThanksForPlayingScene");
    }
}
