using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoSceneManager : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(player.position, this.transform.position) <= 3f) && Input.GetKeyUp(KeyCode.Z))
        {
            SceneManager.LoadScene ("Shop");
        }

        if (Input.GetKeyUp(KeyCode.H))
        {
            SceneManager.LoadScene ("DemoHub");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
