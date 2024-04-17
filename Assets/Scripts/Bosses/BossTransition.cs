using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTransition : MonoBehaviour
{
    public GameObject hud;
    public Camera cam1;
    public Camera cam2;
    public Animator anim1;
    public Animator anim2;

    void Start() 
    {
        cam1.enabled = true;
        cam2.enabled = false;
        hud.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            cam1.enabled = false;
            cam2.enabled = true;
            StartCoroutine(PlayTransition());
        }
    }

    IEnumerator PlayTransition()
    {
        anim1.SetBool("pan", true);
        yield return new WaitForSeconds(1.6f);
        anim2.SetBool("rise", true);
        yield return new WaitForSeconds(0.5f);
        
    }
}
