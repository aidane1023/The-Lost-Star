using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTransition : MonoBehaviour
{
    public GameObject hud;
    public Camera cam1;
    public Camera cam2;
    public Animator anim1;
    public Animator anim2;

    public GameObject player;
    public GameObject part;
    public Collider transition;

    BattleInitiator battleInitiator;

    public AudioSource source;
    public AudioClip bushes;
    public AudioClip hiss;

    public static bool fightHydra = false;

    void Start() 
    {   
        if(fightHydra) part.SetActive(true);
        cam1.enabled = true;
        cam2.enabled = false;
        battleInitiator = GetComponent<BattleInitiator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !fightHydra) 
        {
            player.GetComponent<PlayerController>().speed = 0;
            player.GetComponent<Transform>().position = new Vector3 (185.7f, 0.63f, 3.13f);
            player.GetComponent<Transform>().eulerAngles = new Vector3 (50f, 0f, 0f);
            cam1.enabled = false;
            cam2.enabled = true;
            hud.SetActive(false);
            StartCoroutine(PlayTransition());
        }
    }

    IEnumerator PlayTransition()
    {
        source.PlayOneShot(bushes);
        anim1.SetBool("pan", true);
        yield return new WaitForSeconds(1.6f);
        anim2.SetBool("rise", true);
        yield return new WaitForSeconds(2.1f);
        source.Stop();
        source.PlayOneShot(hiss);
        yield return new WaitForSeconds(0.7f);
        BattleManager.sceneToLoad = 4;
        fightHydra = true;
        transition.enabled = true;
        battleInitiator.InitiateBattle();
    }
}
