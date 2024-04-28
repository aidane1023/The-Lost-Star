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

    public Animator cam;
    public Animator box;
    public Animator crowd;


    void Start()
    {
     StartCoroutine(Sequence());   
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
        GetComponent<BattleInitiator>().InitiateBattle();
    }
}
