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

    public Color newColor;
    public SpriteRenderer starry;
    public GameObject boxObject;
    public GameObject rocketPiece;

    public Animator cam;
    public Animator box;
    public Animator crowd;

    public Camera cam1;
    public Camera cam2;

    public AudioSource transition;
    public AudioSource metro;

    private BattleInitiator battleInitiator;

    private static bool played = false;


    void Start()
    {
        if(played)
        {
            LetsGo();
        }

        cam1.enabled = true;
        cam2.enabled = false;
        battleInitiator = boxObject.GetComponent<BattleInitiator>();
    }

    public void LetsGo()
    {
        cam1.enabled = false;
        cam2.enabled = true;
        if (!played)
        {
            StartCoroutine(OpenBox());
            StartCoroutine(Sequence());
            
        }
        else
        {
            metro.Play();

            
            cam.SetBool("Played", true);
            //boxObject.SetActive(false);
            rocketPiece.SetActive(true);

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
        yield return new WaitForSeconds(0.5f);
        transition.Play();
        set0.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        cam.SetBool("Time", true);
        set1.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        set2.SetActive(true);
        starry.color = newColor;
        yield return new WaitForSeconds(0.5f);
        set3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        set4.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        set5.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        crowd.SetBool("Cheer",true);
        yield return new WaitForSeconds(5.3f);
        BattleManager.sceneToLoad = 6;
        played = true;
        Debug.Log("Start Jack Fight");
        battleInitiator.GetComponent<BattleInitiator>().InitiateBattle();
    }

    IEnumerator OpenBox()
    {
        yield return new WaitForSeconds(3.8f);
        box.SetBool("Open", true);
    }
}
