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

    public AudioSource transition;
    public AudioSource metro;

    private BattleInitiator battleInitiator;

    public static bool played = false;


    void Start()
    {
        

        if (!played)
        {
            battleInitiator = GetComponent<BattleInitiator>();
            StartCoroutine(Sequence());

            transition.enabled = true;
            metro.enabled = false;
        }
        else
        {
            transition.enabled = false;
            metro.enabled = true;
            starry.SetActive(true);
            cam.SetBool("Played", true);
            boxObject.SetActive(false);

            starry.GetComponent<Transform>().position = new Vector3(0.03f,0.4f,2.4f);

            set0.SetActive(true);
            set1.SetActive(true);
            set2.SetActive(true);
            set3.SetActive(true);
            set4.SetActive(true);
            set5.SetActive(true);
            
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
        played = true;
        Debug.Log("Start Jack Fight");
        starry.GetComponent<Transform>().position = new Vector3(0.03f, 0.4f, 2.4f);
        Debug.Log("Player Position " + starry.GetComponent<Transform>().position);
        GetComponent<BattleInitiator>().InitiateBattle();
    }
}
