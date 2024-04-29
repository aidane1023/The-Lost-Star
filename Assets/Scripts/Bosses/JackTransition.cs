using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JackTransition : MonoBehaviour
{
    public GameObject set0;
    public GameObject set1;
    public GameObject set2;
    public GameObject set3;
    public GameObject set4;
    public GameObject set5;

    public GameObject starry;
    public GameObject boxObject;

    public Animator cam;
    public Animator box;
    public Animator crowd;

    private BattleInitiator battleInitiator;

    private static bool tPlayed = false;


    void Start()
    {
        if (!tPlayed)
        {
            battleInitiator = GetComponent<BattleInitiator>();
            StartCoroutine(Sequence());
        }
        else
        {
            cam.SetBool("Played", true);
            starry.GetComponent<Transform>().position = new Vector3(0.0299999993f,0.430000007f,2.41000009f);

            set0.SetActive(true);
            set1.SetActive(true);
            set2.SetActive(true);
            set3.SetActive(true);
            set4.SetActive(true);
            set5.SetActive(true);
            starry.SetActive(true);
        }
           
    }

    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(0.2f);
        set0.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        cam.SetBool("Time", true);
        set1.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        set2.SetActive(true);
        starry.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        set3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        set4.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        set5.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        crowd.SetBool("Cheer",true);
        yield return new WaitForSeconds(0.5f);
        box.SetBool("Open", true);
        yield return new WaitForSeconds(5.5f);
        BattleManager.sceneToLoad = 7;
        tPlayed = true;
        GetComponent<BattleInitiator>().InitiateBattle();
    }
}
