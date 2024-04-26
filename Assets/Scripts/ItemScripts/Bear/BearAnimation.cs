using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAnimation : MonoBehaviour
{
    public Animator anim;
    public Collider range;

    public static int danceSelect = 10;

    public float swingInterval = 15.0f;
    public float wanderInterval = 55.0f;
    public float watchInterval = 3.0f;
    private float currentTime = 1;
    private int roundedTime;

    void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        roundedTime = (int) currentTime;
    
        if (anim.GetBool("OpenIdle") == true)
        {
            if (roundedTime % watchInterval == 0)
            {
                StartCoroutine(Delay(1));
            }
            else if (roundedTime % swingInterval == 0)
            {
                if (Random.Range(0,2) == 1)
                {
                    Debug.Log("Swing");
                    StartCoroutine(Delay(0));
                }
                
            }
        }
        
        if (danceSelect <= 6)
        {
            StartCoroutine(Delay(2));
        }
        
    }

    void OnTriggerEnter()
    {
        anim.SetBool("OpenIdle", false);
        anim.SetBool("Purchase", true);
    }

    void OnTriggerExit()
    {
        anim.SetBool("OpenIdle", true);
        anim.SetBool("Purchase", false);
    }

    IEnumerator Delay(int version)
    {
        switch (version)
        {
            case 0:
                anim.SetBool("Swing", true);
                yield return new WaitForSeconds(1.1f);
                anim.SetBool("OpenIdle", false);
                yield return new WaitForSeconds(4.66f);
                anim.SetBool("OpenIdle", true);
                anim.SetBool("Swing", false);
                break;
            case 1:
                anim.SetBool("LongWait", true);
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("OpenIdle", false);
                yield return new WaitForSeconds(8.25f);
                anim.SetBool("LongWait", false);
                anim.SetBool("OpenIdle", true);
                break;
            case 2:
                anim.SetInteger("DanceSelection", danceSelect);
                yield return new WaitForSeconds(6.7f);
                danceSelect = 10;
                anim.SetInteger("DanceSelection", danceSelect);
                break;
        }
    }
}
